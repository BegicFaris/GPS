import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { MyAppUser } from '../_models/my-app-user';
import { catchError, Observable } from 'rxjs';
import { UserProfile } from '../_models/user-profile';

@Injectable({
  providedIn: 'root'
})
export class MyAppUserService {

   private http = inject(HttpClient);
    private baseUrl = 'https://localhost:5001/api/myappusers';
    myAppUsers: MyAppUser[] = [];
  
  
    getMyAppUserByEmail(email: string): Observable<any>{
      return this.http.get(this.baseUrl + `/get/${email}`)
      ;
    }
    getUserProfile(email: string): Observable<UserProfile> {
      return this.http.get<UserProfile>(`${this.baseUrl}/get/${email}`);
    }
    updateUserProfile(id: number, profile: UserProfile): Observable<UserProfile> {
      console.log(id);
      console.log(profile);
    return this.http.put<UserProfile>(`${this.baseUrl}/profile/${id}`, profile);
    }
    checkEmailExists(email: string): Observable<{ exists: boolean }> {
      return this.http.get<{ exists: boolean }>(`${this.baseUrl}/check-email`, {
        params: { email }
      });
    }
  
}
