import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LoanRequest {
  amount: number;
  tenureMonths: number;
  purpose: string;
}

@Injectable({ providedIn: 'root' })
export class CustomerService {
  private readonly baseUrl = 'http://localhost:5274/api/Customer';

  constructor(private readonly http: HttpClient) {}

  applyLoan(model: LoanRequest): Observable<unknown> {
    return this.http.post(`${this.baseUrl}/apply-loan`, model);
  }
}


