import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { FavoriteLine } from '../_models/favorite-line';

@Injectable({
  providedIn: 'root'
})
export class FavoriteLineService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/favoritelines';

  GetAllFavoriteLines() {
    return this.http.get<FavoriteLine[]>(this.baseUrl);
  }

  GetFavoriteLine(id: number) {
    return this.http.get<FavoriteLine>(this.baseUrl + `/${id}`);
  }
  GetFavoriteLineByUserId(userId: number) {
    return this.http.get<FavoriteLine[]>(this.baseUrl + `/user/${userId}`);
  }

  CreateFavoriteLine(favoriteLine: any) {
    return this.http.post<FavoriteLine>(this.baseUrl, favoriteLine).pipe(
      catchError(error => {
        console.error('Error creating favorite line:', error);
        return (error);
      })
    );
  }

  DeleteFavoriteLine(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting favorite line:', error);
        return (error);
      })
    );
  }
}
