import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { catchError } from 'rxjs';
import { Station } from '../_models/station';

@Injectable({
  providedIn: 'root'
})
export class StationService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/';
  Stations: Station[] = [];
  getAllStations() {

    return this.http.get<Station[]>(this.baseUrl + 'station');
  }
  getStation(id: number){
    return this.http.get<Station>(this.baseUrl + `station/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching station:', error);
        return (error);
      })
    );
  }
  createStation(station: any) {
    return this.http.post<Station>(this.baseUrl + 'station', station).pipe(
      catchError(error => {
        console.error('Error creating station:', error);
        return (error);
      })
    );
  }
  updateStation(station: any) {
    return this.http.put<Station>(this.baseUrl + `station/${station.id}`, station).pipe(
      catchError(error => {
        console.error('Error updating station:', error);
        return (error);
      })
    );
  }
  deleteStation(id: number) {
    return this.http.delete(this.baseUrl + `station/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting station:', error);
        return (error);
      })
    );
  }
}
