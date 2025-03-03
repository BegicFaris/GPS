import { Component, inject, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NotificationService } from '../_services/notification.service';
import { Notification } from '../_models/notification';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-news',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './news.component.html',
  styleUrl: './news.component.css'
})
export class NewsComponent implements OnInit{
  notifications: Notification[] = [];
  currentPage = 1;
  pageSize = 7;
  totalItems = 0;
  
  constructor(public notificationService: NotificationService) {}

  ngOnInit(): void {
    this.loadNotifications();
  }

  loadNotifications(): void {
    this.notificationService.getNotifications(this.currentPage, this.pageSize)
      .subscribe(response => {
        this.notifications = response.items;
        this.totalItems = response.totalCount;
      });
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadNotifications();
    window.scrollTo(0, 0);
  }

  formatDate(date: string): string {
    return new Date(date).toLocaleDateString('en-GB', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric'
    });
  }
  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }
}
