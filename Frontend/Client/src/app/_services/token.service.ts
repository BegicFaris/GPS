import { HttpClient } from "@angular/common/http";
import { Injectable, inject } from "@angular/core";
import { Observable } from "rxjs";


@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/Token';

  getToken(tenantId: string): Observable<any> {
    return this.http.get<string>(`${this.baseUrl}/${tenantId}`);
  }


}
