import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

type AdminSection =
  | 'customers'
  | 'officers'
  | 'loans'
  | 'bg'
  | 'loanVer'
  | 'help'
  | 'feedbackQuestions'
  | 'customerFeedback';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html'
})
export class AdminDashboardComponent implements OnInit {
  active: AdminSection = 'customers';

  customers: any[] = [];
  officers: any[] = [];
  loans: any[] = [];
  bgVerifications: any[] = [];
  loanVerifications: any[] = [];
  helpReports: any[] = [];
  feedbackQuestions: any[] = [];
  customerFeedback: any[] = [];

  selectedBg: any = null;
  selectedLoanVer: any = null;
  newFeedbackQuestion: any = { questionText: '' };

  constructor(
    private readonly admin: AdminService,
    private readonly auth: AuthService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.loadCustomers();
  }

  setSection(section: AdminSection): void {
    this.active = section;
    switch (section) {
      case 'customers':
        this.loadCustomers();
        break;
      case 'officers':
        this.loadOfficers();
        break;
      case 'loans':
        this.loadLoans();
        break;
      case 'bg':
        this.loadBgVerifications();
        break;
      case 'loanVer':
        this.loadLoanVerifications();
        break;
      case 'help':
        this.loadHelpReports();
        break;
      case 'feedbackQuestions':
        this.loadFeedbackQuestions();
        break;
      case 'customerFeedback':
        this.loadCustomerFeedback();
        break;
    }
  }

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  loadCustomers(): void {
    this.admin.getCustomers().subscribe(res => (this.customers = res));
  }

  approveCustomer(id: number): void {
    this.admin.approveCustomer(id).subscribe(() => this.loadCustomers());
  }

  rejectCustomer(id: number): void {
    this.admin.rejectCustomer(id).subscribe(() => this.loadCustomers());
  }

  loadOfficers(): void {
    this.admin.getLoanOfficers().subscribe(res => (this.officers = res));
  }

  approveOfficer(id: number): void {
    this.admin.approveLoanOfficer(id).subscribe(() => this.loadOfficers());
  }

  rejectOfficer(id: number): void {
    this.admin.rejectLoanOfficer(id).subscribe(() => this.loadOfficers());
  }

  loadLoans(): void {
    this.admin.getLoans().subscribe(res => (this.loans = res));
  }

  loadBgVerifications(): void {
    this.admin.getBackgroundVerifications().subscribe(res => (this.bgVerifications = res));
  }

  editBg(v: any): void {
    this.selectedBg = { ...v };
  }

  saveBg(): void {
    if (!this.selectedBg?.id) {
      return;
    }
    this.admin
      .updateBackgroundVerification(this.selectedBg.id, this.selectedBg)
      .subscribe(() => this.loadBgVerifications());
  }

  deleteBg(id: number): void {
    this.admin.deleteBackgroundVerification(id).subscribe(() => this.loadBgVerifications());
  }

  loadLoanVerifications(): void {
    this.admin.getLoanVerifications().subscribe(res => (this.loanVerifications = res));
  }

  editLoanVer(v: any): void {
    this.selectedLoanVer = { ...v };
  }

  saveLoanVer(): void {
    if (!this.selectedLoanVer?.id) {
      return;
    }
    this.admin
      .updateLoanVerification(this.selectedLoanVer.id, this.selectedLoanVer)
      .subscribe(() => this.loadLoanVerifications());
  }

  deleteLoanVer(id: number): void {
    this.admin.deleteLoanVerification(id).subscribe(() => this.loadLoanVerifications());
  }

  loadHelpReports(): void {
    this.admin.getHelpReports().subscribe(res => (this.helpReports = res));
  }

  updateHelpReport(r: any): void {
    if (!r?.id) {
      return;
    }
    this.admin.updateHelpReport(r.id, r).subscribe();
  }

  loadFeedbackQuestions(): void {
    this.admin.getFeedbackQuestions().subscribe(res => (this.feedbackQuestions = res));
  }

  addFeedbackQuestion(): void {
    if (!this.newFeedbackQuestion.questionText) {
      return;
    }
    this.admin.addFeedbackQuestion(this.newFeedbackQuestion).subscribe(() => {
      this.newFeedbackQuestion = { questionText: '' };
      this.loadFeedbackQuestions();
    });
  }

  updateFeedbackQuestion(q: any): void {
    if (!q?.id) {
      return;
    }
    this.admin.updateFeedbackQuestion(q.id, q).subscribe();
  }

  loadCustomerFeedback(): void {
    this.admin.getCustomerFeedback().subscribe(res => (this.customerFeedback = res));
  }
}


