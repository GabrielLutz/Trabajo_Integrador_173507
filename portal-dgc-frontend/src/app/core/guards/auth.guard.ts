import { Injectable } from '@angular/core';
import {
  CanActivate,
  CanLoad,
  Route,
  UrlSegment,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate, CanLoad {
  constructor(private readonly auth: AuthService, private readonly router: Router) {}

  /**
   * Verifica si el usuario está autenticado y, de no ser así, redirige al login conservando la URL solicitada (RF-01).
   */
  private check(url?: string): boolean {
    if (this.auth.isAuthenticated()) {
      return true;
    }
    // Not authenticated; redirect to login with returnUrl
    this.router.navigate(['/login'], { queryParams: { returnUrl: url || '/' } });
    return false;
  }

  /**
   * Evita el acceso a rutas activadas cuando el usuario no tiene sesión válida (RF-01).
   */
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    return this.check(state.url);
  }

  /**
   * Bloquea la carga perezosa de módulos protegidos si el usuario no está autenticado (RF-01).
   */
  canLoad(route: Route, segments: UrlSegment[]): boolean | Observable<boolean> | Promise<boolean> {
    const url = '/' + segments.map((s) => s.path).join('/');
    return this.check(url);
  }
}
