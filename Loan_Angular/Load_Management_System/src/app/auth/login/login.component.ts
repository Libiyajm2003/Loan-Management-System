import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, LoginDto } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  model: LoginDto = { email: '', password: '' };
  error = '';

  constructor(private readonly auth: AuthService, private readonly router: Router) {}

  onSubmit(): void {
    this.error = '';
    this.auth.login(this.model).subscribe({
      next: res => {
        switch (res.role) {
          case 'Admin':
            this.router.navigate(['/admin']);
            break;
          case 'Officer':
            this.router.navigate(['/officer']);
            break;
          default:
            this.router.navigate(['/customer']);
            break;
        }
      },
      error: err => {
        this.error = err.error?.message ?? 'Login failed';
      }
    });
  }
}


