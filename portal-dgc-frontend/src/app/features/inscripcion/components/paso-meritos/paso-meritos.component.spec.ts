import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PasoMeritosComponent } from './paso-meritos.component';

describe('PasoMeritosComponent', () => {
  let component: PasoMeritosComponent;
  let fixture: ComponentFixture<PasoMeritosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PasoMeritosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PasoMeritosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
