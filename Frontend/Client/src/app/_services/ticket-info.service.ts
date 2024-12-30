import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { Station } from '../_models/station';
import { TicketInfo } from '../_models/ticket-info';

@Injectable({
  providedIn: 'root'
})
export class TicketInfoService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/ticketinfo';
  // Get all ticket info
  getAll(): Observable<TicketInfo[]> {
    return this.http.get<TicketInfo[]>(`${this.baseUrl}`);
  }

  // Get ticket info by ID
  getById(id: number): Observable<TicketInfo> {
    return this.http.get<TicketInfo>(`${this.baseUrl}/${id}`);
  }

  // Get ticket info by TicketTypeId
  getByTicketTypeId(ticketTypeId: number): Observable<TicketInfo[]> {
    return this.http.get<TicketInfo[]>(`${this.baseUrl}/by-ticket-type/${ticketTypeId}`);
  }

  // Create a new ticket info
  create(ticketInfo: TicketInfo): Observable<TicketInfo> {
    return this.http.post<TicketInfo>(`${this.baseUrl}`, ticketInfo);
  }

  // Update an existing ticket info
  update(id: number, ticketInfo: TicketInfo): Observable<TicketInfo> {
    return this.http.put<TicketInfo>(`${this.baseUrl}/${id}`, ticketInfo);
  }

  // Delete a ticket info
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
