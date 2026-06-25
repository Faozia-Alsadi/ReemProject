import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { LoginResult } from './models';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly TOKEN_KEY = 'reem_token';
  private readonly USER_KEY  = 'reem_user';

  currentUser = signal<LoginResult | null>(this.loadUser());

  constructor(private http: HttpClient, private router: Router) {}

  login(email: string, password: string) {
    return this.http.post<LoginResult>(`${environment.apiUrl}/auth/login`, { email, password })
      .pipe(tap(result => {
        localStorage.setItem(this.TOKEN_KEY, result.token);
        localStorage.setItem(this.USER_KEY, JSON.stringify(result));
        this.currentUser.set(result);
      }));
  }

  logout() {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.USER_KEY);
    this.currentUser.set(null);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isAdmin(): boolean {
    return this.currentUser()?.role === 'Admin';
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  private loadUser(): LoginResult | null {
    const raw = localStorage.getItem(this.USER_KEY);
    return raw ? JSON.parse(raw) : null;
  }
}
