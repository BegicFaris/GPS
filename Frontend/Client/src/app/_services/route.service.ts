import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Route } from '../_models/route';
import { catchError, Observable } from 'rxjs';

interface StationCountResponse {
  count: number;
}

@Injectable({
  providedIn: 'root'
})
export class RouteService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/routes';
  routes: Route[] = [];

  getAllRoutes() {
    return this.http.get<Route[]>(this.baseUrl);
  }

  getAllRoutesByLineId(lineId: number): Observable<Route[]> {
    return this.http.get<Route[]>(this.baseUrl + `/line/${lineId}`);
}

  getRoute(id: number) {
    return this.http.get<Route>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching route:', error);
        return (error);
      })
    );
  }
  
  getStationCountByLineId(lineId: number){
    return this.http.get<StationCountResponse>(`${this.baseUrl}/station/${lineId}`);
  }
  
  deleteAllRoutesByLineIdAsync(lineId: number) {
    return this.http.delete(this.baseUrl + `/line/${lineId}`).pipe(
      catchError(error => {
        console.error('Error deleting route:', error);
        return (error);
      })
    );
  }
  
  createRoute(route: any) {
    return this.http.post<Route>(this.baseUrl, route).pipe(
      catchError(error => {
        console.error('Error creating route:', error);
        return (error);
      })
    );
  }

  updateRoute(route: any) {
    return this.http.put<Route>(this.baseUrl + `/${route.id}`, route).pipe(
      catchError(error => {
        console.error('Error updating route:', error);
        return (error);
      })
    );
  }

  deleteRoute(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting route:', error);
        return (error);
      })
    );
  }
}
