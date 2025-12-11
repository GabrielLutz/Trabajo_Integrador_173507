import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasoApoyosComponent } from './paso-apoyos.component';

describe('PasoApoyosComponent', () => {
  let component: PasoApoyosComponent;
  let fixture: ComponentFixture<PasoApoyosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PasoApoyosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PasoApoyosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
