import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { Station } from '../_models/station';
import { TicketInfo } from '../_models/ticket-info';
import { TicketType } from '../_models/ticket-type';

@Injectable({
  providedIn: 'root'
})
export class TicketTypeService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/tickettype';
  // Get all ticket info
  getAll(): Observable<TicketType[]> {
    return this.http.get<TicketType[]>(this.baseUrl);
  }

  getById(id: number): Observable<TicketType> {
    return this.http.get<TicketType>(`${this.baseUrl}/${id}`);
  }

  create(ticketType: TicketType): Observable<TicketType> {
    return this.http.post<TicketType>(this.baseUrl, ticketType);
  }

  update(id: number, ticketType: TicketType): Observable<TicketType> {
    return this.http.put<TicketType>(`${this.baseUrl}/${id}`, ticketType);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
