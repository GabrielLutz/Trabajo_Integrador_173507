import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasoAutodefinicionComponent } from './paso-autodefinicion.component';

describe('PasoAutodefinicionComponent', () => {
  let component: PasoAutodefinicionComponent;
  let fixture: ComponentFixture<PasoAutodefinicionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PasoAutodefinicionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PasoAutodefinicionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
