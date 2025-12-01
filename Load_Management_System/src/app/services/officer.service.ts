import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class OfficerService {
  private readonly baseUrl = 'http://localhost:5274/api/Officer';

  constructor(private readonly http: HttpClient) {}

  getAssignedLoans(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/assigned-loans`);
  }

  updateLoanVerification(loanId: number, body: any): Observable<unknown> {
    return this.http.post(`${this.baseUrl}/update-loan-verification/${loanId}`, body);
  }

  updateBackgroundVerification(loanId: number, body: any): Observable<unknown> {
    return this.http.post(
      `${this.baseUrl}/update-background-verification/${loanId}`,
      body
    );
  }

  getHelpReports(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/help-reports`);
  }
}


