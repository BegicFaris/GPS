import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { catchError } from 'rxjs';
import { Station } from '../_models/station';

@Injectable({
  providedIn: 'root'
})
export class StationService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/stations';
  Stations: Station[] = [];
  getAllStations() {
    return this.http.get<Station[]>(this.baseUrl );
  }
  getStation(id: number){
    return this.http.get<Station>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching station:', error);
        return (error);
      })
    );
  }
  createStation(station: any) {
    return this.http.post<Station>(this.baseUrl, station).pipe(
      catchError(error => {
        console.error('Error creating station:', error);
        return (error);
      })
    );
  }
  updateStation(station: any) {
    return this.http.put<Station>(this.baseUrl + `/${station.id}`, station).pipe(
      catchError(error => {
        console.error('Error updating station:', error);
        return (error);
      })
    );
  }
  deleteStation(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting station:', error);
        return (error);
      })
    );
  }
}
