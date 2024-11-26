import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LineDTO } from './line-dto.model';  // For create/update
import { Line } from './line.model';  // For full object (with ID)

@Injectable({
  providedIn: 'root'
})
export class LineService {
  private apiUrl = 'https://localhost:5001/api/lines';  // Adjust to your API URL

  constructor(private http: HttpClient) {}

  // Create a new line (using LineDTO, which doesn't include id)
  createLine(line: LineDTO): Observable<any> {
    return this.http.post<any>(this.apiUrl, line);  // Send only LineDTO for creation
  }

  // Update an existing line (send Line model with id)
  updateLine(line: Line): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${line.id}`, line);  // Send full Line (with id) for update
  }

  // Get a line by its ID (for editing)
  getLineById(id: number): Observable<Line> {
    return this.http.get<Line>(`${this.apiUrl}/${id}`);
  }

  // Get all lines (returns an array of lines)
  getAllLines(): Observable<Line[]> {
    return this.http.get<Line[]>(this.apiUrl);  // Get all lines from the backend
  }

  // Delete a line by its ID
  deleteLine(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);  // Delete line by ID
  }
}
