import { Component } from '@angular/core';
import { CustomerService, LoanRequest } from '../../services/customer.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html'
})
export class CustomerDashboardComponent {
  model: LoanRequest = {
    amount: 0,
    tenureMonths: 0,
    purpose: ''
  };

  successMessage = '';
  errorMessage = '';

  constructor(
    private readonly customer: CustomerService,
    private readonly auth: AuthService,
    private readonly router: Router
  ) {}

  applyLoan(): void {
    this.successMessage = '';
    this.errorMessage = '';

    this.customer.applyLoan(this.model).subscribe({
      next: res => {
        const anyRes = res as any;
        this.successMessage =
          anyRes?.message ?? 'Loan request submitted successfully';
        this.model = { amount: 0, tenureMonths: 0, purpose: '' };
      },
      error: err => {
        this.errorMessage = err.error?.message ?? 'Failed to submit loan request';
      }
    });
  }

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}


