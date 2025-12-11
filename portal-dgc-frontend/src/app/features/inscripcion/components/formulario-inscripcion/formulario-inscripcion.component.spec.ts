import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { InscripcionService } from '../../../../core/services/inscripcion.service';
import { LlamadoService } from '../../../../core/services/llamado.service';
import { PostulanteService } from '../../../../core/services/postulante.service';
import { FormularioInscripcionComponent } from './formulario-inscripcion.component';

describe('FormularioInscripcionComponent', () => {
  let component: FormularioInscripcionComponent;
  let fixture: ComponentFixture<FormularioInscripcionComponent>;

  beforeEach(async () => {
    const llamadoDetalleMock = {
      success: true,
      data: {
        requisitosExcluyentes: [],
        itemsPuntuables: [],
        apoyosNecesarios: [],
        departamentos: []
      }
    };

    await TestBed.configureTestingModule({
      declarations: [FormularioInscripcionComponent],
      imports: [ReactiveFormsModule, FormsModule, RouterTestingModule],
      providers: [
        { provide: ActivatedRoute, useValue: { queryParams: of({ llamadoId: 1 }) } },
        { provide: Router, useValue: { navigate: jasmine.createSpy('navigate') } },
        {
          provide: InscripcionService,
          useValue: {
            crearInscripcion: jasmine.createSpy('crearInscripcion').and.returnValue(of({ success: true, data: { id: 1 } }))
          }
        },
        {
          provide: LlamadoService,
          useValue: {
            obtenerLlamadoDetalle: jasmine.createSpy('obtenerLlamadoDetalle').and.returnValue(of(llamadoDetalleMock))
          }
        },
        {
          provide: PostulanteService,
          useValue: {
            obtenerPostulante: jasmine.createSpy('obtenerPostulante').and.returnValue(of({ success: true, data: { datosCompletados: true } })),
            validarCedulaDisponible: jasmine.createSpy('validarCedulaDisponible').and.returnValue(of({ success: true, data: true }))
          }
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormularioInscripcionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
