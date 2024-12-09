import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor() {}

  login(email: string, password: string): Observable<boolean> {
    // This is a mock implementation. In a real application, you would
    // send these credentials to your backend for verification.
    return of(email === 'user@example.com' && password === 'password').pipe(
      delay(1000) // Simulate network latency
    );
  }
}