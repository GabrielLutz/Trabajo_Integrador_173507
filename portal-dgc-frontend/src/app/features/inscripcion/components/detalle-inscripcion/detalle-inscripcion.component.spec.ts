import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, convertToParamMap } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { SharedModule } from '../../../../shared/shared.module';
import { InscripcionService } from '../../../../core/services/inscripcion.service';
import { DetalleInscripcionComponent } from './detalle-inscripcion.component';

describe('DetalleInscripcionComponent', () => {
  let component: DetalleInscripcionComponent;
  let fixture: ComponentFixture<DetalleInscripcionComponent>;

  beforeEach(async () => {
    const inscripcionResponse = {
      success: true,
      message: '',
      errors: [],
      data: {
        id: 1,
        postulanteId: 1,
        nombrePostulante: 'Postulante Demo',
        llamadoId: 10,
        tituloLlamado: 'Llamado de prueba',
        departamentoId: 5,
        nombreDepartamento: 'Montevideo',
        fechaInscripcion: new Date(),
        estado: 'Pendiente',
        puntajeTotal: 0,
        autodefinicion: {
          esAfrodescendiente: false,
          esTrans: false,
          tieneDiscapacidad: false
        },
        requisitos: [],
        meritos: [],
        apoyos: []
      }
    };

    const inscripcionServiceMock = {
      obtenerInscripcion: jasmine.createSpy('obtenerInscripcion').and.returnValue(of(inscripcionResponse))
    };

    await TestBed.configureTestingModule({
      declarations: [DetalleInscripcionComponent],
      imports: [SharedModule, RouterTestingModule],
      providers: [
        { provide: InscripcionService, useValue: inscripcionServiceMock },
        {
          provide: ActivatedRoute,
          useValue: {
            paramMap: of(convertToParamMap({ id: '1' }))
          }
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalleInscripcionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
