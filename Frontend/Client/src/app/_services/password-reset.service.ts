// password-reset.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PasswordResetService {
  private apiUrl = 'https://localhost:5001/api/passwordreset';

  constructor(private http: HttpClient) {}

  sendResetCode(email: string): Observable<any> {
     const emailObject = { email }; 
    return this.http.post(`${this.apiUrl}/send-code`, emailObject);
  }

  verifyCode(email: string, code: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/verify-code`, { email, code });
  }
  resetPassword(
    email: string,
    code: string,
    newPassword: string
  ): Observable<any> {
    return this.http.post(`${this.apiUrl}/reset-password`, {
      email,
      code,
      newPassword,
    });
  }
}
