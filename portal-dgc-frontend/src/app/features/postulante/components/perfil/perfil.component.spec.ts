import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { PostulanteService } from '../../../../core/services/postulante.service';
import { PerfilComponent } from './perfil.component';

describe('PerfilComponent', () => {
  let component: PerfilComponent;
  let fixture: ComponentFixture<PerfilComponent>;
  let postulanteService: jasmine.SpyObj<PostulanteService>;

  beforeEach(async () => {
    postulanteService = jasmine.createSpyObj<PostulanteService>('PostulanteService', ['obtenerPostulante']);
    postulanteService.obtenerPostulante.and.returnValue(of({
      success: true,
      data: {
        id: 1,
        nombre: 'Gabriel',
        apellido: 'Lutz',
        fechaNacimiento: new Date('1994-11-02'),
        cedulaIdentidad: '4.567.890-1',
        genero: 'Masculino',
        email: 'gabriel@example.com',
        celular: '098765432',
        domicilio: 'Av. Libertador 999',
        datosCompletados: true
      }
    } as any));

    await TestBed.configureTestingModule({
      declarations: [PerfilComponent],
      imports: [RouterTestingModule],
      providers: [{ provide: PostulanteService, useValue: postulanteService }],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(PerfilComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
