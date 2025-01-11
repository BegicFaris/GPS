// shift-detail.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { ShiftDetail } from '../_models/shift-detail'; // Adjust path accordingly

@Injectable({
  providedIn: 'root'
})
export class ShiftDetailService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/shiftdetails';


  GetAllShiftDetails() {
    return this.http.get<ShiftDetail[]>(this.baseUrl);
  }

  GetShiftDetailsByShiftId(shiftId: number) {
    return this.http.get<ShiftDetail[]>(`${this.baseUrl}/shift/${shiftId}`);
  }

  GetShiftDetail(id: number) {
    return this.http.get<ShiftDetail>(`${this.baseUrl}/${id}`);
  }

  CreateShiftDetail(shiftDetail: ShiftDetail) {
    return this.http.post<ShiftDetail>(this.baseUrl, shiftDetail).pipe(
      catchError(error => {
        console.error('Error creating shift detail:', error);
        return error;
      })
    );
  }

  DeleteShiftDetail(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting shift detail:', error);
        return error;
      })
    );
  }
  DeleteShiftDetailsByShiftId(shiftId: number) {
    return this.http.delete(`${this.baseUrl}/shift/${shiftId}`).pipe(
      catchError(error => {
        console.error('Error deleting shift details:', error);
        return error;
      })
    );
  }
  GetShiftDetailPdf(shiftId:number):Observable<Blob>{
    return this.http.get<Blob>(`${this.baseUrl}/generate-pdf/${shiftId}`, { responseType: 'blob' as 'json' });
  }

}
