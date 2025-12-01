import { Component, OnInit } from '@angular/core';
import { OfficerService } from '../../services/officer.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-officer-dashboard',
  templateUrl: './officer-dashboard.component.html'
})
export class OfficerDashboardComponent implements OnInit {
  loans: any[] = [];
  helpReports: any[] = [];

  selectedLoanId: number | null = null;
  loanVerification: any = { status: '', remarks: '' };
  bgVerification: any = { status: '', remarks: '' };

  constructor(
    private readonly officer: OfficerService,
    private readonly auth: AuthService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.loadAssignedLoans();
    this.loadHelpReports();
  }

  loadAssignedLoans(): void {
    this.officer.getAssignedLoans().subscribe(res => (this.loans = res));
  }

  loadHelpReports(): void {
    this.officer.getHelpReports().subscribe(res => (this.helpReports = res));
  }

  selectLoan(loan: any): void {
    this.selectedLoanId = loan.id;
    this.loanVerification = { status: '', remarks: '' };
    this.bgVerification = { status: '', remarks: '' };
  }

  saveLoanVerification(): void {
    if (!this.selectedLoanId) {
      return;
    }
    this.officer
      .updateLoanVerification(this.selectedLoanId, this.loanVerification)
      .subscribe(() => this.loadAssignedLoans());
  }

  saveBgVerification(): void {
    if (!this.selectedLoanId) {
      return;
    }
    this.officer
      .updateBackgroundVerification(this.selectedLoanId, this.bgVerification)
      .subscribe(() => this.loadAssignedLoans());
  }

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}


