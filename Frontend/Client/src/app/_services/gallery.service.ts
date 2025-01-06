// gallery.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Gallery } from '../_models/gallery';

@Injectable({
    providedIn: 'root'
})
export class GalleryService {

    private apiUrl = 'https://localhost:5001/api/gallery';

    constructor(private http: HttpClient) {}
    gallery: Gallery[] = [];

    // Upload a photo
    uploadPhoto(gallery: Gallery): Observable<Gallery> {
        return this.http.post<Gallery>(`${this.apiUrl}/upload`, gallery);
    }

    // Get a single photo by ID
    getPhoto(id: number): Observable<Gallery> {
        return this.http.get<Gallery>(`${this.apiUrl}/${id}`);
    }

    // Get all photos
    getAllPhotos()
     {
        return this.http.get<Gallery[]>(this.apiUrl);
    }

    // Delete a photo by ID
    deletePhoto(id: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
    // Update the order of photos
    updatePhotoOrder(updatedOrder: { id: number, newPosition: number }[]): Observable<any> {
        return this.http.put(`${this.apiUrl}/order`, updatedOrder);
    }
}
