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

  /**
   * Ejecuta un inicio de sesión simulado alineado con RF-01 "Autenticación de usuario" para pruebas de UI.
   * Acepta cualquier combinación usuario/contraseña no vacía y persiste la sesión en localStorage.
   */
  login(usuario: string, password: string): boolean {
    // Simulación simple: acepta cualquier usuario/contraseña no vacíos
    if (usuario && password) {
      const user: SimulatedUser = { usuario };
      localStorage.setItem(this.storageKey, JSON.stringify(user));
      return true;
    }
    return false;
  }

  /**
   * Limpia la sesión simulada almacenada en localStorage (RF-01).
   */
  logout(): void {
    localStorage.removeItem(this.storageKey);
  }

  /**
   * Checks if a simulated session exists in localStorage.
   */
  isAuthenticated(): boolean {
    return !!localStorage.getItem(this.storageKey);
  }

  /**
   * Retrieves the currently stored simulated user or null when no session exists.
   */
  getCurrentUser(): SimulatedUser | null {
    const raw = localStorage.getItem(this.storageKey);
    return raw ? JSON.parse(raw) : null;
  }

  /**
   * Guarda un usuario simulado en localStorage para emular el registro sin backend (RF-02).
   */
  register(payload: SimulatedUser & { usuario: string; password?: string }): boolean {
    // Guardar usuario simulado en localStorage (no impacta DB)
    const usersRaw = localStorage.getItem(this.usersKey) || '[]';
    const users = JSON.parse(usersRaw) as Array<any>;
    users.push(payload);
    localStorage.setItem(this.usersKey, JSON.stringify(users));
    return true;
  }
}
