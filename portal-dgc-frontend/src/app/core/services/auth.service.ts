import { Injectable } from '@angular/core';

export interface SimulatedUser {
  usuario: string;
  nombre?: string;
  apellido?: string;
  cedula?: string;
  email?: string;
  celular?: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly storageKey = 'portal-dgc-current-user';
  private readonly usersKey = 'portal-dgc-users';

  login(usuario: string, password: string): boolean {
    // Simulación simple: acepta cualquier usuario/contraseña no vacíos
    if (usuario && password) {
      const user: SimulatedUser = { usuario };
      localStorage.setItem(this.storageKey, JSON.stringify(user));
      return true;
    }
    return false;
  }

  logout(): void {
    localStorage.removeItem(this.storageKey);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem(this.storageKey);
  }

  getCurrentUser(): SimulatedUser | null {
    const raw = localStorage.getItem(this.storageKey);
    return raw ? JSON.parse(raw) : null;
  }

  register(payload: SimulatedUser & { usuario: string; password?: string }): boolean {
    // Guardar usuario simulado en localStorage (no impacta DB)
    const usersRaw = localStorage.getItem(this.usersKey) || '[]';
    const users = JSON.parse(usersRaw) as Array<any>;
    users.push(payload);
    localStorage.setItem(this.usersKey, JSON.stringify(users));
    return true;
  }
}
