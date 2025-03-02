import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Manager } from '../_models/manager';
import { catchError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/managers';
  managers: Manager[] = [];

  getAllManagers() {
    return this.http.get<Manager[]>(this.baseUrl);
  }

  getManager(id: number) {
    return this.http.get<Manager>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching manager:', error);
        return (error);
      })
    );
  }

  createManager(manager: any) {
    return this.http.post<Manager>(this.baseUrl, manager).pipe(
      catchError(error => {
        console.error('Error creating manager:', error);
        return (error);
      })
    );
  }

  updateManager(manager: any) {
    return this.http.put<Manager>(this.baseUrl + `/${manager.id}`, manager).pipe(
      catchError(error => {
        console.error('Error updating manager:', error);
        return (error);
      })
    );
  }

  deleteManager(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting manager:', error);
        return (error);
      })
    );
  }

  managerCheckEmailExists(email: string): Observable<{ exists: boolean }> {
    return this.http.get<{ exists: boolean }>(`${this.baseUrl}/check-email?email=${email}`);
  }
}
