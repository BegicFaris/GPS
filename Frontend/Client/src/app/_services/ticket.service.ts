import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Ticket } from '../_models/ticket';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/tickets';
  tickets: Ticket[] = [];

  getAllTickets() {
    return this.http.get<Ticket[]>(this.baseUrl);
  }

  getTicket(id: number) {
    return this.http.get<Ticket>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching ticket:', error);
        return (error);
      })
    );
  }

  createTicket(ticket: any) {
    return this.http.post<Ticket>(this.baseUrl, ticket).pipe(
      catchError(error => {
        console.error('Error creating ticket:', error);
        return (error);
      })
    );
  }

  updateTicket(ticket: any) {
    return this.http.put<Ticket>(this.baseUrl + `/${ticket.id}`, ticket).pipe(
      catchError(error => {
        console.error('Error updating ticket:', error);
        return (error);
      })
    );
  }

  deleteTicket(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting ticket:', error);
        return (error);
      })
    );
  }
}
