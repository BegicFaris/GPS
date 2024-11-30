import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Feedback } from '../_models/feedback';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/feedbacks';
  feedbacks: Feedback[] = [];

  getAllFeedbacks() {
    return this.http.get<Feedback[]>(this.baseUrl);
  }

  getFeedback(id: number) {
    return this.http.get<Feedback>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching feedback:', error);
        return (error);
      })
    );
  }

  createFeedback(feedback: any) {
    return this.http.post<Feedback>(this.baseUrl, feedback).pipe(
      catchError(error => {
        console.error('Error creating feedback:', error);
        return (error);
      })
    );
  }

  updateFeedback(feedback: any) {
    return this.http.put<Feedback>(this.baseUrl + `/${feedback.id}`, feedback).pipe(
      catchError(error => {
        console.error('Error updating feedback:', error);
        return (error);
      })
    );
  }

  deleteFeedback(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting feedback:', error);
        return (error);
      })
    );
  }
}
