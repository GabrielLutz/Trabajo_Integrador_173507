import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  menuAbierto = false;

  constructor(private readonly router: Router) {}

  toggleMenu(): void {
    this.menuAbierto = !this.menuAbierto;
  }

  navegarA(ruta: string): void {
    this.router.navigate([ruta]);
    this.menuAbierto = false;
  }
}
