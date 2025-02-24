import { inject, Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, switchMap, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { TokenService } from './token.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private router = inject(Router);
  private tokenService = inject(TokenService);
  private url: string = window.location.href;
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const user = localStorage.getItem('user');
    if (user) {
      const userObj = JSON.parse(user);
      const token = userObj.token;
      const authRequest = request.clone({
        headers: request.headers.set('Authorization', `Bearer ${token}`),
      });
      return next.handle(authRequest);
    } else {
      console.log('Request url:' + request.url);
      {
        const baseUrl = this.getBaseUrl(this.url);

        if (baseUrl == 'https://localhost:4200') {
          const authRequest = request.clone({
            headers: request.headers.set('Tenant', `mostar`),
          });
          return next.handle(authRequest);
        }
        else if (baseUrl == 'https://localhost:4202') {
          const authRequest = request.clone({
            headers: request.headers.set('Tenant', `sarajevo`),
          });
          return next.handle(authRequest);
        }
      }
      return next.handle(request);
    }
  }
  getBaseUrl(fullUrl: string): string {
    const url = new URL(fullUrl);
    return `${url.protocol}//${url.host}`;
  }
}
