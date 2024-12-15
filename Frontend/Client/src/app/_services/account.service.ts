import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { catchError, map } from 'rxjs';
import { Manager } from '../_models/manager';

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
      })
    );
  }

  getUserRole(): string | null{
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user).role :null;
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
