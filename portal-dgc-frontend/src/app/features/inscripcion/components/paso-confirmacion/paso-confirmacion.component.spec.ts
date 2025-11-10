import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasoConfirmacionComponent } from './paso-confirmacion.component';

describe('PasoConfirmacionComponent', () => {
  let component: PasoConfirmacionComponent;
  let fixture: ComponentFixture<PasoConfirmacionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PasoConfirmacionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PasoConfirmacionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
