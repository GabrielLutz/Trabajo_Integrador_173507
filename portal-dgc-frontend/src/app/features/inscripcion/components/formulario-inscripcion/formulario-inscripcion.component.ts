import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, QueryList, ViewChildren } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, Subscription, fromEvent } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';
import { ApiResponse } from '../../../../core/models/api-response.model';
import { CrearInscripcion, MeritoPostulante, RequisitoPostulante } from '../../../../core/models/inscripcion.model';
import { ItemPuntuable, LlamadoDetalle, RequisitoExcluyente } from '../../../../core/models/llamado.model';
import { InscripcionService } from '../../../../core/services/inscripcion.service';
import { LlamadoService } from '../../../../core/services/llamado.service';
import { PostulanteService } from '../../../../core/services/postulante.service';

@Component({
  selector: 'app-formulario-inscripcion',
  standalone: false,
  templateUrl: './formulario-inscripcion.component.html',
  styleUrls: ['./formulario-inscripcion.component.scss']
})
export class FormularioInscripcionComponent implements OnInit, OnDestroy, AfterViewInit {
  @ViewChildren('fileInput')
  private fileInputs!: QueryList<ElementRef<HTMLInputElement>>;
  private fileInputSubscriptions: Subscription[] = [];
  pasoActual = 0;
  llamadoId!: number;
  postulanteId = 1;
  aceptaTerminos = false;

  llamado: LlamadoDetalle | null = null;
  loading = false;
  error = '';

  readonly pasos = [
    { titulo: 'Departamento', icono: '📍' },
    { titulo: 'Autodefinición', icono: '👤' },
    { titulo: 'Requisitos', icono: '✓' },
    { titulo: 'Méritos', icono: '⭐' },
    { titulo: 'Apoyos', icono: '🤝' },
    { titulo: 'Confirmación', icono: '✓' }
  ];

  readonly formDepartamento: FormGroup<DepartamentoForm>;
  readonly formAutodefinicion: FormGroup<AutodefinicionForm>;
  readonly formRequisitos: FormGroup<RequisitosForm>;
  readonly formMeritos: FormGroup<MeritosForm>;
  readonly formApoyos: FormGroup<ApoyosForm>;

  private readonly destroy$ = new Subject<void>();

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly inscripcionService: InscripcionService,
    private readonly llamadoService: LlamadoService,
    private readonly postulanteService: PostulanteService
  ) {
    this.formDepartamento = new FormGroup<DepartamentoForm>({
      departamentoId: new FormControl<number | null>(null, {
        validators: [Validators.required]
      })
    });

    this.formAutodefinicion = new FormGroup<AutodefinicionForm>({
      esAfrodescendiente: new FormControl(false, { nonNullable: true }),
      esTrans: new FormControl(false, { nonNullable: true }),
      tieneDiscapacidad: new FormControl(false, { nonNullable: true })
    });

    this.formRequisitos = new FormGroup<RequisitosForm>({
      requisitos: new FormArray<RequisitoFormGroup>([])
    });

    this.formMeritos = new FormGroup<MeritosForm>({
      meritos: new FormArray<MeritoFormGroup>([])
    });

    this.formApoyos = new FormGroup<ApoyosForm>({
      apoyosSeleccionados: new FormArray<FormControl<boolean>>([])
    });
  }

  /**
   * Obtiene parámetros iniciales y dispara la carga del llamado y las validaciones (RF-05).
   */
  ngOnInit(): void {
    this.route.queryParams.pipe(takeUntil(this.destroy$)).subscribe((params) => {
      const idParam = params['llamadoId'];
      const parsedId = Number(idParam);

      if (!Number.isNaN(parsedId) && parsedId > 0) {
        this.llamadoId = parsedId;
        this.cargarLlamado();
        this.validarPostulante();
      } else {
        this.router.navigate(['/llamados']);
      }
    });
  }

  /**
   * Registra listeners de archivos una vez que Angular renderiza los inputs de méritos.
   */
  ngAfterViewInit(): void {
    this.fileInputs.changes
      .pipe(startWith(this.fileInputs), takeUntil(this.destroy$))
      .subscribe((inputs) => this.registrarListenersArchivo(inputs));
  }

  /**
   * Libera recursos asociados al formulario cuando se destruye el componente.
   */
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Arreglo reactivo con los requisitos del llamado.
   */
  get requisitosArray(): FormArray<RequisitoFormGroup> {
    return this.formRequisitos.controls.requisitos;
  }

  /**
   * Arreglo reactivo con los méritos disponibles para cargar respaldos.
   */
  get meritosArray(): FormArray<MeritoFormGroup> {
    return this.formMeritos.controls.meritos;
  }

  /**
   * Arreglo reactivo con los apoyos que el postulante puede solicitar.
   */
  get apoyosArray(): FormArray<FormControl<boolean>> {
    return this.formApoyos.controls.apoyosSeleccionados;
  }

  /**
   * Recupera el detalle del llamado e inicializa cada paso del formulario (RF-04/RF-05).
   */
  cargarLlamado(): void {
    this.loading = true;
    this.llamadoService.obtenerLlamadoDetalle(this.llamadoId).subscribe({
      next: (response: ApiResponse<LlamadoDetalle>) => {
        if (response.success && response.data) {
          this.llamado = response.data;
          this.prepararFormularios();
        } else {
          this.error = 'No se encontró información del llamado.';
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'Error al cargar el llamado.';
        this.loading = false;
      }
    });
  }

  /**
   * Verifica que el postulante tenga sus datos completos antes de continuar (RF-02).
   */
  validarPostulante(): void {
    this.postulanteService.obtenerPostulante(this.postulanteId).subscribe({
      next: (response) => {
        if (response.success && response.data && !response.data.datosCompletados) {
          window.alert('Debe completar sus datos personales antes de inscribirse.');
          this.router.navigate(['/perfil', 'editar']);
        }
      }
    });
  }

  /**
   * Inicializa los formularios dinámicos de requisitos, méritos y apoyos.
   */
  prepararFormularios(): void {
    if (!this.llamado) {
      return;
    }

    this.requisitosArray.clear();
    this.meritosArray.clear();
    this.apoyosArray.clear();

    this.llamado.requisitosExcluyentes.forEach((req) => {
      this.requisitosArray.push(this.crearRequisitoGroup(req));
    });

    this.llamado.itemsPuntuables.forEach((item) => {
      this.meritosArray.push(this.crearMeritoGroup(item));
    });

    this.llamado.apoyosNecesarios.forEach(() => {
      this.apoyosArray.push(new FormControl(false, { nonNullable: true }));
    });
  }

  /**
   * Avanza al siguiente paso del flujo guiado validando el formulario actual.
   */
  siguientePaso(): void {
    const formActual = this.obtenerFormActual();

    if (!formActual) {
      if (this.pasoActual < this.pasos.length - 1) {
        this.pasoActual++;
      }
      return;
    }

    if (formActual.valid) {
      if (this.pasoActual < this.pasos.length - 1) {
        this.pasoActual++;
      }
    } else {
      this.marcarCamposComoTocados(formActual);
      window.alert('Por favor complete todos los campos requeridos.');
    }
  }

  /**
   * Retrocede al paso inmediato anterior.
   */
  pasoAnterior(): void {
    if (this.pasoActual > 0) {
      this.pasoActual--;
    }
  }

  /**
   * Permite navegar a un paso ya completado para revisar información.
   */
  irAPaso(paso: number): void {
    if (paso <= this.pasoActual) {
      this.pasoActual = paso;
    }
  }

  private registrarListenersArchivo(inputs: QueryList<ElementRef<HTMLInputElement>>): void {
    this.fileInputSubscriptions.forEach((subscription) => subscription.unsubscribe());
    this.fileInputSubscriptions = inputs.map((input, index) =>
      fromEvent(input.nativeElement, 'change')
        .pipe(takeUntil(this.destroy$))
        .subscribe(() => this.actualizarDocumentoRespaldo(index, input.nativeElement))
    );
  }

  private actualizarDocumentoRespaldo(index: number, input: HTMLInputElement | null): void {
    const control = this.meritosArray.at(index)?.controls.documentoRespaldo;

    if (!control) {
      return;
    }

    if (!input?.files?.length) {
      control.setValue('');
      return;
    }

    const file = input.files[0];
    control.setValue(file.name);
  }

  /**
   * Construye el payload y lo envía al backend validando términos y campos requeridos (RF-05).
   */
  enviarInscripcion(): void {
    if (!this.aceptaTerminos) {
      window.alert('Debe aceptar los términos y condiciones.');
      return;
    }

    const departamentoId = this.formDepartamento.controls.departamentoId.value;

    if (departamentoId === null) {
      this.marcarCamposComoTocados(this.formDepartamento);
      window.alert('Seleccione un departamento antes de continuar.');
      return;
    }

    const inscripcion: CrearInscripcion = {
      llamadoId: this.llamadoId,
      departamentoId,
      autodefinicion: this.formAutodefinicion.getRawValue(),
      requisitos: this.obtenerRequisitos(),
      meritos: this.obtenerMeritos(),
      apoyosIds: this.obtenerApoyosSeleccionados()
    };

    this.loading = true;
    this.error = '';

    this.inscripcionService
      .crearInscripcion(this.postulanteId, inscripcion)
      .subscribe({
        next: (response) => {
          if (response.success && response.data) {
            window.alert('¡Inscripción realizada con éxito!');
            this.router.navigate(['/inscripcion', response.data.id]);
          } else {
            this.error = 'No fue posible crear la inscripción.';
          }
          this.loading = false;
        },
        error: (err) => {
          this.error = err.error?.message || 'Error al crear la inscripción.';
          this.loading = false;
          window.alert(this.error);
        }
      });
  }

  /**
   * Devuelve el nombre del departamento seleccionado para mostrar en la UI.
   */
  obtenerNombreDepartamento(): string {
    if (!this.llamado) {
      return '';
    }

    const selectedId = this.formDepartamento.controls.departamentoId.value;
    const departamento = this.llamado.departamentos.find(
      (d) => d.departamentoId === selectedId
    );

    return departamento?.nombre ?? '';
  }

  /**
   * Cantidad de requisitos marcados como cumplidos por el postulante.
   */
  contarRequisitos(): number {
    return this.requisitosArray.controls.filter(
      (control) => control.controls.cumple.value === true
    ).length;
  }

  /**
   * Cantidad de méritos seleccionados para enviar al backend.
   */
  contarMeritos(): number {
    return this.meritosArray.controls.filter(
      (control) => control.controls.seleccionado.value
    ).length;
  }

  /**
   * Cantidad de apoyos solicitados en el paso correspondiente.
   */
  contarApoyos(): number {
    return this.apoyosArray.controls.filter((control) => control.value).length;
  }

  /**
   * Cancela la inscripción y regresa al detalle del llamado actual.
   */
  cancelar(): void {
    if (window.confirm('¿Está seguro que desea cancelar la inscripción?')) {
      this.router.navigate(['/llamados', this.llamadoId]);
    }
  }

  private obtenerFormActual(): FormGroup | null {
    switch (this.pasoActual) {
      case 0:
        return this.formDepartamento;
      case 1:
        return this.formAutodefinicion;
      case 2:
        return this.formRequisitos;
      case 3:
        return this.formMeritos;
      case 4:
        return this.formApoyos;
      default:
        return null;
    }
  }

  private marcarCamposComoTocados(form: FormGroup): void {
    Object.values(form.controls).forEach((control) => {
      if (control instanceof FormControl) {
        control.markAsTouched();
      } else if (control instanceof FormGroup) {
        this.marcarCamposComoTocados(control);
      } else if (control instanceof FormArray) {
        control.controls.forEach((child) => {
          if (child instanceof FormGroup) {
            this.marcarCamposComoTocados(child);
          } else if (child instanceof FormControl) {
            child.markAsTouched();
          }
        });
      }
    });
  }

  private obtenerRequisitos(): RequisitoPostulante[] {
    return this.requisitosArray.controls.map((control) => ({
      requisitoId: control.controls.requisitoId.value,
      cumple: control.controls.cumple.value === true,
      observaciones: control.controls.observaciones.value || undefined
    }));
  }

  private obtenerMeritos(): MeritoPostulante[] {
    return this.meritosArray.controls
      .filter((control) => control.controls.seleccionado.value)
      .map((control) => ({
        itemPuntuableId: control.controls.itemPuntuableId.value,
        documentoRespaldo: control.controls.documentoRespaldo.value || undefined
      }));
  }

  private obtenerApoyosSeleccionados(): number[] {
    const apoyos = this.llamado?.apoyosNecesarios ?? [];

    return this.apoyosArray.controls
      .map((control, index) => (control.value ? apoyos[index]?.id ?? null : null))
      .filter((id): id is number => id !== null);
  }

  private crearRequisitoGroup(req: RequisitoExcluyente): RequisitoFormGroup {
    return new FormGroup<RequisitoForm>({
      requisitoId: new FormControl(req.id, { nonNullable: true }),
      cumple: new FormControl<boolean | null>(null, { validators: [Validators.required] }),
      observaciones: new FormControl('', { nonNullable: true })
    });
  }

  private crearMeritoGroup(item: ItemPuntuable): MeritoFormGroup {
    return new FormGroup<MeritoForm>({
      itemPuntuableId: new FormControl(item.id, { nonNullable: true }),
      seleccionado: new FormControl(false, { nonNullable: true }),
      documentoRespaldo: new FormControl('', { nonNullable: true })
    });
  }

}


type DepartamentoForm = {
  departamentoId: FormControl<number | null>;
};

type AutodefinicionForm = {
  esAfrodescendiente: FormControl<boolean>;
  esTrans: FormControl<boolean>;
  tieneDiscapacidad: FormControl<boolean>;
};

type RequisitoForm = {
  requisitoId: FormControl<number>;
  cumple: FormControl<boolean | null>;
  observaciones: FormControl<string>;
};

type MeritoForm = {
  itemPuntuableId: FormControl<number>;
  seleccionado: FormControl<boolean>;
  documentoRespaldo: FormControl<string>;
};

type ApoyosForm = {
  apoyosSeleccionados: FormArray<FormControl<boolean>>;
};

type RequisitosForm = {
  requisitos: FormArray<RequisitoFormGroup>;
};

type MeritosForm = {
  meritos: FormArray<MeritoFormGroup>;
};

type RequisitoFormGroup = FormGroup<RequisitoForm>;

type MeritoFormGroup = FormGroup<MeritoForm>;
