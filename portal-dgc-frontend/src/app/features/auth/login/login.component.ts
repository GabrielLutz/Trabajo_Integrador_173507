import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: false
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  loading = false;
  error = '';
  successMessage = '';

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

  ngOnInit(): void {
    this.route.queryParams.subscribe((qp) => {
      if (qp['registered']) {
        this.successMessage = 'Registro simulado completado. Podés ingresar con tu usuario.';
      }
    });
  }

  get f() {
    return this.form.controls;
  }

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
      const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/llamados';
      this.router.navigateByUrl(returnUrl);
    } else {
      this.error = 'Usuario o contraseña inválidos (simulado)';
    }
  }

  goToRegistro(): void {
    this.router.navigate(['/login/registro']);
  }
}
