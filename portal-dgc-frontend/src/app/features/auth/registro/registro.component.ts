import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-registro',
  standalone: false,
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.scss']
})
export class RegistroComponent {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly auth = inject(AuthService);

  loading = false;

  form = this.fb.nonNullable.group({
    nombre: ['', Validators.required],
    apellido: ['', Validators.required],
    cedulaIdentidad: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    celular: ['', Validators.required],
    usuario: ['', Validators.required],
    contrasena: ['', [Validators.required, Validators.minLength(6)]]
  });

  /**
   * Controles expuestos para simplificar el template.
   */
  get f() {
    return this.form.controls;
  }

  /**
   * Formatea la cédula de identidad en tiempo real respetando el patrón uruguayo.
   */
  onCedulaInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    const digits = input.value.replace(/\D/g, '').slice(0, 8);
    const formatted = this.formatCedula(digits);
    this.form.get('cedulaIdentidad')?.setValue(formatted, { emitEvent: false });
    input.value = formatted;
    input.setSelectionRange(formatted.length, formatted.length);
  }

  private formatCedula(digits: string): string {
    if (!digits) {
      return '';
    }

    const segments = [1, 3, 3, 1];
    const parts: string[] = [];
    let offset = 0;

    for (let i = 0; i < segments.length && offset < digits.length; i++) {
      const next = Math.min(offset + segments[i], digits.length);
      parts.push(digits.slice(offset, next));
      offset = next;
    }

    let formatted = parts[0] ?? '';

    if (parts.length > 1) {
      formatted += '.' + parts[1];
    }
    if (parts.length > 2) {
      formatted += '.' + parts[2];
    }
    if (parts.length > 3) {
      formatted += '-' + parts[3];
    }

    return formatted;
  }

  /**
   * Realiza el registro simulado y redirige al login con mensaje de confirmación (RF-02).
   */
  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    const raw = this.form.getRawValue();
    const payload = {
      usuario: raw.usuario,
      nombre: raw.nombre,
      apellido: raw.apellido,
      email: raw.email,
      cedula: raw.cedulaIdentidad,
      celular: raw.celular,
      password: raw.contrasena
    };

    this.auth.register(payload);

    setTimeout(() => {
      this.loading = false;
      const { usuario } = this.form.getRawValue();
      this.router.navigate(['/login'], {
        queryParams: { registered: '1', user: usuario }
      });
    }, 800);
  }
}
