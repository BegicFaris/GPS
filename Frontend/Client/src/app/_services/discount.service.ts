import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { Discount } from '../_models/discount';

@Injectable({
  providedIn: 'root'
})
export class DiscountService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/discounts';
  discounts: Discount[] = [];

  getAllDiscounts() {
    return this.http.get<Discount[]>(this.baseUrl);
  }

  getDiscount(id: number) {
    return this.http.get<Discount>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching discount:', error);
        return (error);
      })
    );
  }

  createDiscount(discount: any) {
      return this.http.post<Discount>(this.baseUrl, discount).pipe(
        catchError(error => {
          console.error('Error creating discount:', error);
          return (error);
        })
      );
    }
  
    updateDiscount(discount: any) {
      return this.http.put<Discount>(this.baseUrl + `/${discount.id}`, discount).pipe(
        catchError(error => {
          console.error('Error updating discount:', error);
          return (error);
        })
      );
    }
  
    deleteDiscount(id: number) {
      return this.http.delete(this.baseUrl + `/${id}`).pipe(
        catchError(error => {
          console.error('Error deleting discount:', error);
          return (error);
        })
      );
    }

}
