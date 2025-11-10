import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasoRequisitosComponent } from './paso-requisitos.component';

describe('PasoRequisitosComponent', () => {
  let component: PasoRequisitosComponent;
  let fixture: ComponentFixture<PasoRequisitosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PasoRequisitosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PasoRequisitosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
