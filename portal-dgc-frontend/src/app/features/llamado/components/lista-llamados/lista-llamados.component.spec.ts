import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaLlamadosComponent } from './lista-llamados.component';

describe('ListaLlamadosComponent', () => {
  let component: ListaLlamadosComponent;
  let fixture: ComponentFixture<ListaLlamadosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListaLlamadosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaLlamadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
