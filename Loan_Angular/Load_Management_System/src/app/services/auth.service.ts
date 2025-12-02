import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';

export interface RegisterDto {
  fullName: string;
  email: string;
  password: string;
  role?: 'Admin' | 'Customer' | 'Officer';
}

export interface LoginDto {
  email: string;
  password: string;
}

export interface AuthResponseDto {
  email: string;
  role: string;
  token: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly apiUrl = 'http://localhost:5274/api/Auth';
  private currentUserSubject = new BehaviorSubject<AuthResponseDto | null>(null);
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {
    const saved = localStorage.getItem('auth');
    if (saved) {
      this.currentUserSubject.next(JSON.parse(saved) as AuthResponseDto);
    }
  }

  register(model: RegisterDto): Observable<unknown> {
    return this.http.post(`${this.apiUrl}/register`, model);
  }

  login(model: LoginDto): Observable<AuthResponseDto> {
    return this.http.post<AuthResponseDto>(`${this.apiUrl}/login`, model).pipe(
      tap(res => {
        localStorage.setItem('auth', JSON.stringify(res));
        this.currentUserSubject.next(res);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('auth');
    this.currentUserSubject.next(null);
  }

  get token(): string | null {
    return this.currentUserSubject.value?.token ?? null;
  }

  get role(): string | null {
    return this.currentUserSubject.value?.role ?? null;
  }

  get isLoggedIn(): boolean {
    return this.token != null;
  }
}


