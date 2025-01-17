import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Ticket } from '../_models/ticket';
import { catchError, Observable } from 'rxjs';

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

  getTicketsOverTime(): Observable<{ month: string; count: number }[]> {
    return this.http.get<{ month: string; count: number }[]>(`${this.baseUrl}/tickets-over-time`);
  }  

  getTicketByEmail(email: string): Observable<any>{
        return this.http.get(this.baseUrl + `/get/${email}`).pipe(
          catchError(error => {
            return (error);
          })
        );
      }
  getTicket(id: number) {
    return this.http.get<Ticket>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching ticket:', error);
        return (error);
      })
    );
  }
  
  getUserTickets(email: string): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(`${this.baseUrl}/get/${email}`);
  }

  getUserTicketsPaginated(
    email: string, 
    pageNumber: number = 1, 
    pageSize: number = 5
  ): Observable<PagintedTickets> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
    return this.http.get<PagintedTickets>(`${this.baseUrl}/get/${email}/paginated`, { params });
  }
  createTicket(ticket: any) {
    return this.http.post<Ticket>(this.baseUrl, ticket).pipe(
      catchError(error => {
        console.error('Error creating ticket:', error);
        return (error);
      })
    );
  }

  createTicketWithStripe(ticketData: any): Observable<Ticket> {
    return this.http.post<Ticket>(`${this.baseUrl}/create-with-payment`, ticketData);
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

interface PagintedTickets {
  totalTickets: number;
  totalPages: number;
  currentPage: number;
  pageSize: number;
  tickets: Ticket[];
}