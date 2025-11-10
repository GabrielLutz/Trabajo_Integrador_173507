import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { PostulanteService } from '../../../../core/services/postulante.service';
import { DatosPersonalesComponent } from './datos-personales.component';

describe('DatosPersonalesComponent', () => {
  let component: DatosPersonalesComponent;
  let fixture: ComponentFixture<DatosPersonalesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DatosPersonalesComponent],
      imports: [ReactiveFormsModule, RouterTestingModule],
      providers: [
        {
          provide: PostulanteService,
          useValue: {
            obtenerPostulante: jasmine.createSpy('obtenerPostulante').and.returnValue(of({
              success: true,
              data: {
                nombre: 'Nombre',
                apellido: 'Apellido',
                fechaNacimiento: new Date().toISOString(),
                cedulaIdentidad: '1.234.567-8',
                genero: 'Masculino',
                generoOtro: null,
                email: 'test@example.com',
                celular: '099123456',
                telefono: null,
                domicilio: 'Calle 123'
              }
            })),
            validarCedulaDisponible: jasmine.createSpy('validarCedulaDisponible').and.returnValue(of({ success: true, data: true })),
            actualizarDatosPersonales: jasmine.createSpy('actualizarDatosPersonales').and.returnValue(of({ success: true }))
          }
        },
        { provide: Router, useValue: { navigate: jasmine.createSpy('navigate') } }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DatosPersonalesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
