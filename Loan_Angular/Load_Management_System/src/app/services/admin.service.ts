import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AdminService {
  private readonly baseUrl = 'http://localhost:5274/api/Admin';

  constructor(private readonly http: HttpClient) {}

  getCustomers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/customers`);
  }

  approveCustomer(id: number): Observable<unknown> {
    return this.http.put(`${this.baseUrl}/customers/${id}/approve`, {});
  }

  rejectCustomer(id: number): Observable<unknown> {
    return this.http.put(`${this.baseUrl}/customers/${id}/reject`, {});
  }

  getLoanOfficers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/loan-officers`);
  }

  approveLoanOfficer(id: number): Observable<unknown> {
    return this.http.put(`${this.baseUrl}/loan-officers/${id}/approve`, {});
  }

  rejectLoanOfficer(id: number): Observable<unknown> {
    return this.http.put(`${this.baseUrl}/loan-officers/${id}/reject`, {});
  }

  assignOfficerForBg(verificationId: number, officerId: number): Observable<unknown> {
    return this.http.post(
      `${this.baseUrl}/loan-officers/assign-bg/${verificationId}/${officerId}`,
      {}
    );
  }

  assignOfficerForLoanVerification(verificationId: number, officerId: number): Observable<unknown> {
    return this.http.post(
      `${this.baseUrl}/loan-officers/assign-loan-verification/${verificationId}/${officerId}`,
      {}
    );
  }

  getLoans(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/loans`);
  }

  getBackgroundVerifications(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/background-verifications`);
  }

  updateBackgroundVerification(id: number, body: any): Observable<unknown> {
    return this.http.put(`${this.baseUrl}/background-verifications/${id}`, body);
  }

  deleteBackgroundVerification(id: number): Observable<unknown> {
    return this.http.delete(`${this.baseUrl}/background-verifications/${id}`);
  }

  getLoanVerifications(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/loan-verifications`);
  }

  updateLoanVerification(id: number, body: any): Observable<unknown> {
    return this.http.put(`${this.baseUrl}/loan-verifications/${id}`, body);
  }

  deleteLoanVerification(id: number): Observable<unknown> {
    return this.http.delete(`${this.baseUrl}/loan-verifications/${id}`);
  }

  getHelpReports(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/help-reports`);
  }

  updateHelpReport(id: number, body: any): Observable<unknown> {
    return this.http.put(`${this.baseUrl}/help-reports/${id}`, body);
  }

  addFeedbackQuestion(body: any): Observable<unknown> {
    return this.http.post(`${this.baseUrl}/feedback-questions`, body);
  }

  getFeedbackQuestions(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/feedback-questions`);
  }

  updateFeedbackQuestion(id: number, body: any): Observable<unknown> {
    return this.http.put(`${this.baseUrl}/feedback-questions/${id}`, body);
  }

  getCustomerFeedback(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/customer-feedback`);
  }
}


