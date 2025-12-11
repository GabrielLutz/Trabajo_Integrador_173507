import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { SharedModule } from '../../../../shared/shared.module';
import { InscripcionService } from '../../../../core/services/inscripcion.service';
import { MisInscripcionesComponent } from './mis-inscripciones.component';

describe('MisInscripcionesComponent', () => {
  let component: MisInscripcionesComponent;
  let fixture: ComponentFixture<MisInscripcionesComponent>;
  const inscripcionesResponse = {
    success: true,
    message: '',
    data: [],
    errors: []
  };

  beforeEach(async () => {
    const inscripcionServiceMock = {
      obtenerInscripcionesPorPostulante: jasmine
        .createSpy('obtenerInscripcionesPorPostulante')
        .and.returnValue(of(inscripcionesResponse))
    };

    await TestBed.configureTestingModule({
      declarations: [MisInscripcionesComponent],
      imports: [SharedModule, RouterTestingModule],
      providers: [{ provide: InscripcionService, useValue: inscripcionServiceMock }],
      schemas: [NO_ERRORS_SCHEMA]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MisInscripcionesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
