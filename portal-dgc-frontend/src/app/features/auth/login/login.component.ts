import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: false
})
export class LoginComponent implements OnInit, OnDestroy {
  form: FormGroup;
  loading = false;
  error = '';
  successMessage = '';
  private redirectUrl: string | null = null;
  private readonly destroy$ = new Subject<void>();

  constructor(
    private readonly fb: FormBuilder,
    private readonly auth: AuthService,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      usuario: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  /**
   * Recupera parámetros de la ruta para mostrar mensajes y configurar el returnUrl.
   */
  ngOnInit(): void {
    this.route.queryParams.pipe(takeUntil(this.destroy$)).subscribe((params) => {
      if (params['registered']) {
        this.successMessage = 'Registro simulado completado. Podés ingresar con tu usuario.';
      }

      if (params['returnUrl']) {
        this.redirectUrl = params['returnUrl'];
        const otros = { ...params };
        delete otros['returnUrl'];
        this.router.navigate([], {
          relativeTo: this.route,
          queryParams: otros,
          replaceUrl: true
        });
      }
    });
  }

  /**
   * Exposición conveniente de los controles del formulario para el template.
   */
  get f() {
    return this.form.controls;
  }

  /**
   * Ejecuta el login simulado utilizando AuthService y redirige según corresponda (RF-01).
   */
  submit(): void {
    this.error = '';
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const usuario = this.f['usuario'].value;
    const password = this.f['password'].value;
    this.loading = true;
    const ok = this.auth.login(usuario, password);
    this.loading = false;
    if (ok) {
      const returnUrl = this.redirectUrl || '/llamados';
      this.redirectUrl = null;
      this.router.navigateByUrl(returnUrl);
    } else {
      this.error = 'Usuario o contraseña inválidos (simulado)';
    }
  }

  /**
   * Cancela suscripciones activas para evitar fugas de memoria.
   */
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Navega al formulario de registro simulado.
   */
  goToRegistro(): void {
    this.router.navigate(['/login/registro']);
  }
}
