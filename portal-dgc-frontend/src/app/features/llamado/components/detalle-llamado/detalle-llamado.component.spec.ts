import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalleLlamadoComponent } from './detalle-llamado.component';

describe('DetalleLlamadoComponent', () => {
  let component: DetalleLlamadoComponent;
  let fixture: ComponentFixture<DetalleLlamadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalleLlamadoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalleLlamadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
