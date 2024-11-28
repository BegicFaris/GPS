import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Line } from '../_models/lines/line';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LineService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/';
  Lines: Line[] = [];
  getAllLines() {

    return this.http.get<Line[]>(this.baseUrl + 'lines');
  }
  getLine(id: number) {
    return this.http.get<Line>(this.baseUrl + `lines/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching line:', error);
        return (error);
      })
    );
  }
  createLine(line: any) {
    return this.http.post<Line>(this.baseUrl + 'lines', line).pipe(
      catchError(error => {
        console.error('Error creating line:', error);
        return (error);
      })
    );
  }
  updateLine(line: any) {
    return this.http.put<Line>(this.baseUrl + `lines/${line.Id}`, line).pipe(
      catchError(error => {
        console.error('Error updating line:', error);
        return (error);
      })
    );
  }
  deleteLine(id: number) {
    return this.http.delete(this.baseUrl + `lines/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting line:', error);
        return (error);
      })
    );
  }
}
