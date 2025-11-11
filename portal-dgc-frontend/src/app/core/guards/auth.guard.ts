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

  private check(url?: string): boolean {
    if (this.auth.isAuthenticated()) {
      return true;
    }
    // Not authenticated; redirect to login with returnUrl
    this.router.navigate(['/login'], { queryParams: { returnUrl: url || '/' } });
    return false;
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    return this.check(state.url);
  }

  canLoad(route: Route, segments: UrlSegment[]): boolean | Observable<boolean> | Promise<boolean> {
    const url = '/' + segments.map((s) => s.path).join('/');
    return this.check(url);
  }
}
