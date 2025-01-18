import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Line } from '../_models/line';
import { catchError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LineService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/lines';
  lines: Line[] = [];

  getAllLines(lineName?: string|null, stationName?: string|null) {
    // Create query parameters based on input
    let params = new HttpParams();
    if (lineName) {
      params = params.append('lineName', lineName);
    }
    if (stationName) {
      params = params.append('stationName', stationName);
    }
  
    // Make the GET request with query parameters
    return this.http.get<Line[]>(this.baseUrl, { params });
  }
  getAllLinesByStationId(stationId: number) {
    return this.http.get<Line[]>(this.baseUrl + `/station/${stationId}`);
  }


  getLine(id: number) {
    return this.http.get<Line>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching line:', error);
        return (error);
      })
    );
  }

  createLine(line: any) {
    return this.http.post<Line>(this.baseUrl, line).pipe(
      catchError(error => {
        console.error('Error creating line:', error);
        return (error);
      })
    );
  }

  updateLine(line: any) {
    return this.http.put<Line>(this.baseUrl + `/${line.id}`, line).pipe(
      catchError(error => {
        console.error('Error updating line:', error);
        return (error);
      })
    );
  }

  deleteLine(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting line:', error);
        return (error);
      })
    );
  }

  getSchedulePDF():Observable<Blob>{
    return this.http.get<Blob>(`${this.baseUrl}/generate-pdf`, { responseType: 'blob' as 'json' });
  }

}
