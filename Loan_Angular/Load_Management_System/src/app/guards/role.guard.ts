import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  constructor(private readonly auth: AuthService, private readonly router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRoles: string[] = route.data['roles'] ?? [];
    const currentRole = this.auth.role;

    if (!this.auth.isLoggedIn || !currentRole || !expectedRoles.includes(currentRole)) {
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
}


