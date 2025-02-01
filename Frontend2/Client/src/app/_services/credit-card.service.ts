import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CreditCard } from '../_models/credit-card';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CreditCardService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/creditcards';
  creditCards: CreditCard[] = [];

  getAllCreditCards() {
    return this.http.get<CreditCard[]>(this.baseUrl);
  }

  getCreditCard(id: number) {
    return this.http.get<CreditCard>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching credit card:', error);
        return (error);
      })
    );
  }

  createCreditCard(creditCard: any) {
    return this.http.post<CreditCard>(this.baseUrl, creditCard).pipe(
      catchError(error => {
        console.error('Error creating credit card:', error);
        return (error);
      })
    );
  }

  updateCreditCard(creditCard: any) {
    return this.http.put<CreditCard>(this.baseUrl + `/${creditCard.id}`, creditCard).pipe(
      catchError(error => {
        console.error('Error updating credit card:', error);
        return (error);
      })
    );
  }

  deleteCreditCard(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting credit card:', error);
        return (error);
      })
    );
  }
}
