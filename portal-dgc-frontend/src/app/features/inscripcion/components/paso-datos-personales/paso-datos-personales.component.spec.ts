import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasoDatosPersonalesComponent } from './paso-datos-personales.component';

describe('PasoDatosPersonalesComponent', () => {
  let component: PasoDatosPersonalesComponent;
  let fixture: ComponentFixture<PasoDatosPersonalesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PasoDatosPersonalesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PasoDatosPersonalesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
