import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { Discount } from '../_models/discount';
import { Zone } from '../_models/zone';

@Injectable({
  providedIn: 'root'
})
export class ZoneService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/zones';
  zones: Zone[] = [];

  getAllZones() {
    return this.http.get<Zone[]>(this.baseUrl);
  }

  getZone(id: number) {
    return this.http.get<Zone>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching zone:', error);
        return (error);
      })
    );
  }

}
