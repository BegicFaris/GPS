import { Component, inject } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [
    BsDropdownModule,
    FormsModule,
    CommonModule,
    RouterLink,
    RouterLinkActive,
  ],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  showPassword: boolean = false;
  accountService = inject(AccountService);
  private router = inject(Router);
  model: any = {};

  get userRole(): string | null{
    return this.accountService.getUserRole();
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: (_) => {
        this.router.navigateByUrl('/home');
        
      },
      error: (error) => console.log(error),
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

notifications = [
    {
      type: 'warning',
      iconClass: 'bi bi-exclamation-circle-fill text-warning',
      count: 2,
      details: ['Low battery', 'Overdue maintenance'],
      showDetails: false,
    },
    {
      type: 'caution',
      iconClass: 'bi bi-exclamation-triangle-fill text-danger',
      count: 1,
      details: ['Speeding detected'],
      showDetails: false,
    },
    {
      type: 'info',
      iconClass: 'bi bi-info-circle-fill text-info',
      count: 5,
      details: ['Route updated', 'Weather change', 'New message'],
      showDetails: false,
    },
  ];

  showDetails(type: string): void {
    const notification = this.notifications.find((n) => n.type === type);
    if (notification) {
      notification.showDetails = true;
    }
  }

  hideDetails(type: string): void {
    const notification = this.notifications.find((n) => n.type === type);
    if (notification) {
      notification.showDetails = false;
    }
  }

}
