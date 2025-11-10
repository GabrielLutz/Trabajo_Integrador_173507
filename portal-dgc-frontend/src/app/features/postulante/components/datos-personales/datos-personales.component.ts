import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorMessageComponent } from '../../../../shared/components/error-message/error-message.component';
import { LoadingComponent } from '../../../../shared/components/loading/loading.component';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, takeUntil } from 'rxjs/operators';
import { PostulanteDatosPersonales } from '../../../../core/models/postulante.model';
import { PostulanteService } from '../../../../core/services/postulante.service';

type DatosPersonalesForm = {
  nombre: FormControl<string>;
  apellido: FormControl<string>;
  fechaNacimiento: FormControl<string>;
  cedulaIdentidad: FormControl<string>;
  genero: FormControl<string>;
  generoOtro: FormControl<string | null>;
  email: FormControl<string>;
  celular: FormControl<string>;
  telefono: FormControl<string | null>;
  domicilio: FormControl<string>;
};

@Component({
  selector: 'app-datos-personales',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, LoadingComponent, ErrorMessageComponent],
  templateUrl: './datos-personales.component.html',
  styleUrls: ['./datos-personales.component.scss']
})
export class DatosPersonalesComponent implements OnInit, OnDestroy {
  readonly form: FormGroup<DatosPersonalesForm>;
  loading = false;
  error = '';
  success = false;
  postulanteId = 1;

  readonly generos = ['Masculino', 'Femenino', 'Otro'];
  private readonly destroy$ = new Subject<void>();

  constructor(
    private readonly postulanteService: PostulanteService,
    protected readonly router: Router
  ) {
    this.form = new FormGroup<DatosPersonalesForm>({
      nombre: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required, Validators.maxLength(100)]
      }),
      apellido: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required, Validators.maxLength(100)]
      }),
      fechaNacimiento: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required]
      }),
      cedulaIdentidad: new FormControl('', {
        nonNullable: true,
        validators: [
          Validators.required,
          Validators.pattern(/^(\d{1}\.\d{3}\.\d{3}-\d{1}|\d{7,8})$/)
        ]
      }),
      genero: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required]
      }),
      generoOtro: new FormControl<string | null>('', { validators: [] }),
      email: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required, Validators.email]
      }),
      celular: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required, Validators.pattern(/^0\d{8}$/)]
      }),
      telefono: new FormControl<string | null>('', {
        validators: [Validators.pattern(/^0\d{8}$/)]
      }),
      domicilio: new FormControl('', {
        nonNullable: true,
        validators: [Validators.required, Validators.maxLength(200)]
      })
    });
  }

  ngOnInit(): void {
    this.configurarFormateoCedula();
    this.cargarDatos();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  cargarDatos(): void {
    this.loading = true;
    this.postulanteService.obtenerPostulante(this.postulanteId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          const {
            nombre,
            apellido,
            fechaNacimiento,
            cedulaIdentidad,
            genero,
            generoOtro,
            email,
            celular,
            telefono,
            domicilio
          } = response.data;

          this.form.patchValue({
            nombre,
            apellido,
            fechaNacimiento: this.toInputDate(fechaNacimiento),
            cedulaIdentidad,
            genero,
            generoOtro: generoOtro ?? '',
            email,
            celular,
            telefono: telefono ?? '',
            domicilio
          }, { emitEvent: false });
        }
        this.loading = false;
      },
      error: () => {
        this.error = 'Error al cargar datos';
        this.loading = false;
      }
    });
  }

  onGeneroChange(): void {
    const genero = this.form.get('genero')?.value;
    const generoOtroControl = this.form.controls.generoOtro;
    if (genero === 'Otro') {
      generoOtroControl?.setValidators([Validators.required]);
    } else {
      generoOtroControl?.clearValidators();
      generoOtroControl?.setValue('');
    }
    generoOtroControl?.updateValueAndValidity();
  }

  validarCedula(): void {
    const cedula = this.form.controls.cedulaIdentidad.value;
    if (cedula) {
      this.postulanteService.validarCedulaDisponible(cedula).subscribe({
        next: (response) => {
          const control = this.form.controls.cedulaIdentidad;
          if (response.data === false) {
            const errors = { ...(control.errors ?? {}) };
            errors['noDisponible'] = true;
            control.setErrors(errors);
          } else {
            if (control.errors?.['noDisponible']) {
              const { ['noDisponible']: _removed, ...others } = control.errors;
              control.setErrors(Object.keys(others).length ? others : null);
            }
          }
        }
      });
    }
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.marcarCamposComoTocados();
      return;
    }

    this.loading = true;
    this.error = '';

    const {
      nombre,
      apellido,
      fechaNacimiento,
      cedulaIdentidad,
      genero,
      generoOtro,
      email,
      celular,
      telefono,
      domicilio
    } = this.form.getRawValue();

    const datos: PostulanteDatosPersonales = {
      nombre,
      apellido,
      fechaNacimiento: new Date(fechaNacimiento),
      cedulaIdentidad,
      genero,
      generoOtro: genero === 'Otro' ? generoOtro ?? '' : undefined,
      email,
      celular,
      telefono: telefono ?? undefined,
      domicilio
    };

    this.postulanteService.actualizarDatosPersonales(this.postulanteId, datos).subscribe({
      next: (response) => {
        if (response.success) {
          this.success = true;
          setTimeout(() => {
            this.router.navigate(['/llamados']);
          }, 2000);
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = err.error?.message || 'Error al actualizar datos';
        this.loading = false;
      }
    });
  }

  marcarCamposComoTocados(): void {
    Object.values(this.form.controls).forEach((control) => control.markAsTouched());
  }

  getErrorMessage(fieldName: string): string {
    const field = this.form.controls[fieldName as keyof DatosPersonalesForm];
    if (field?.hasError('required')) {
      return 'Este campo es requerido';
    }
    if (field?.hasError('email')) {
      return 'Email inválido';
    }
    if (field?.hasError('pattern')) {
      return 'Formato inválido';
    }
    if (field?.hasError('noDisponible')) {
      return 'Esta cédula ya está registrada';
    }
    return '';
  }

  private configurarFormateoCedula(): void {
    this.form.controls.cedulaIdentidad.valueChanges
      .pipe(takeUntil(this.destroy$))
      .subscribe((value) => {
        const digits = value.replace(/\D/g, '');
        let formatted = digits;

        if (digits.length >= 7) {
          const base = `${digits.substring(0, 1)}.${digits.substring(1, 4)}.${digits.substring(4, 7)}`;
          const verifier = digits.substring(7, 8);
          formatted = verifier ? `${base}-${verifier}` : base;
        }

        if (formatted !== value) {
          this.form.controls.cedulaIdentidad.setValue(formatted, { emitEvent: false });
        }
      });

    this.form.controls.cedulaIdentidad.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        filter((value) => value.replace(/\D/g, '').length >= 7),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.validarCedula());
  }

  private toInputDate(value: string | Date): string {
    const date = value instanceof Date ? value : new Date(value);
    if (Number.isNaN(date.getTime())) {
      return '';
    }
    return date.toISOString().substring(0, 10);
  }
}
