import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { catchError, map, throwError } from 'rxjs';
import { Manager } from '../_models/manager';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);

  baseUrl= 'https://localhost:5001/api/';

  currentUser = signal<User | null>(null);

  login(model: any){
    console.log(model);
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
    let errorMessage = 'Invalid email or password';
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
  getUserRole(): string | null{
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user).role :null;
  }
  getUserEmail(): string{
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user).email :null;
  }

  register(model: any){
    console.log(model);
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
    this.currentUser.set(null);
  }

}
