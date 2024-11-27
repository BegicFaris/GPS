import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { catchError, map, Observable, throwError } from 'rxjs';
import { Tenant } from '../_models/tenant';


@Injectable({
  providedIn: 'root'
})
export class TenantService {
  private http = inject(HttpClient);

  baseUrl= 'https://localhost:5001/api/';

  fetchTenants(): Observable<Tenant[]> {
    return this.http.get<Tenant[]>(this.baseUrl + 'tenant/tenant').pipe(
      catchError(this.handleError) // Handle errors
    );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMsg: string;
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMsg = `An error occurred: ${error.error.message}`;
    } else {
      // Server-side error
      errorMsg = `Backend returned code ${error.status}, body was: ${error.error}`;
    }
    console.error(errorMsg);
    return throwError(() => new Error(errorMsg));
  }
  
}