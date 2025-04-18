import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { catchError, map, Observable, throwError } from 'rxjs';
import { Manager } from '../_models/manager';

interface TwoFAStatus {
  twoFactorStatus: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);

  baseUrl= 'https://localhost:5001/api/';

  currentUser = signal<User | null>(null);

  login(model: any){
    return this.http.post<User>(this.baseUrl + 'account/login', model)
    .pipe(
      map(user => {
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      }),
      catchError(this.handleError)
    );
  }


  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Client-side or network error
      errorMessage = error.error.message;
    } else {
      // Backend error
      if (error.status === 401) {
        errorMessage = 'Invalid email or password';
      } else if (error.status === 500) {
        errorMessage = 'An internal server error occurred. Please try again later.';
      }
    }
    return throwError(() => new Error(errorMessage));
  }

  

  get2FAStatus(email: string): Observable<TwoFAStatus> {
    return this.http.get<TwoFAStatus>(`${this.baseUrl}twofactorauth/get-2fa-status`, {
      params: new HttpParams().set('email', email),
    }).pipe(
      catchError(this.handleError)
    );
  }

  sendResetCode(email: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}twofactorauth/send-code`, { email }).pipe(
      catchError(this.handleError)
    );
  }

  verifyCode(email: string, code: string): Observable<any> {
    return this.http.post(`${this.baseUrl}twofactorauth/verify-code`, { email, code }).pipe(
      catchError(this.handleError)
    );
  }
  
  getUserRole(): string | null{
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user).role :null;
  }

  getUserId(): string | null{
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user).id :null;
  }
  getUserEmail(): string{
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user).email : '';
  }

  register(model: any){
    return this.http.post<User>(this.baseUrl + 'account/register/passenger', model)
    .pipe(
      map(user => {
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      })
    );
  }

  registerManager(model: any){
    return this.http.post<User>(this.baseUrl + 'account/register/manager', model).pipe(
      catchError(error => {
        console.error('Error creating manager:', error);
        return (error);
      })
    );
  }

  registerDriver(model: any){
    return this.http.post<User>(this.baseUrl + 'account/register/driver', model).pipe(
      catchError(error => {
        console.error('Error creating driver:', error);
        return (error);
      })
    );
  }
  
  logout(){
    localStorage.removeItem('user');
    localStorage.removeItem('_grecaptcha');
    this.currentUser.set(null);
  }

}
