import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { MyAppUserService } from '../_services/my-app-user.service';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { MyAppUser } from '../_models/my-app-user';
import { ThemeService } from '../_services/theme.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Notification } from '../_models/notification';
import { NotificationService } from '../_services/notification.service';

interface GroupedNotifications {
  type: string;
  count: number;
  notifications: Notification[];
  showDetails: boolean;
}

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [
    BsDropdownModule,
    FormsModule,
    CommonModule,
    RouterLink,
    RouterLinkActive,
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent implements OnInit {
  showPassword: boolean = false;
  accountService = inject(AccountService);
  myAppUserService = inject(MyAppUserService);
  router = inject(Router);
  model: any = {};
  user: any = {};
  email: string = '';
  loginError: string = '';
  isLoading: boolean = false;
  showTwoFactorInput: boolean = false;
  twoFactorCode: string = '';
  codeVerified: boolean = false;
  codeError: string = '';
  invalidCode: boolean = false;
  groupedNotifications: GroupedNotifications[] = [];
  currentlyVisibleNotification: GroupedNotifications | null = null;

  constructor(
    public themeService: ThemeService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.userEmail(); // Get email after login
    this.loadName();
    this.loadNotifications();
  }

  get userRole(): string | null {
    return this.accountService.getUserRole();
  }
  toggleTheme() {
    this.themeService.toggleTheme();
  }
  loadNotifications() {
    this.notificationService.getRecentNotifications().subscribe({
      next: (notifications) => {
        this.groupNotifications(notifications);
      },
      error: (error) => {
        console.error('Error fetching notifications:', error);
      },
    });
  }
  groupNotifications(notifications: Notification[]) {
    const grouped = notifications.reduce((acc, notification) => {
      const type = notification.notificationType.name;
      if (!acc[type]) {
        acc[type] = {
          type,
          count: 0,
          notifications: [],
          showDetails: false,
        };
      }
      acc[type].count++;
      acc[type].notifications.push(notification);
      return acc;
    }, {} as Record<string, GroupedNotifications>);

    this.groupedNotifications = Object.values(grouped);
  }

  loadName() {
    this.myAppUserService.getMyAppUserByEmail(this.email).subscribe((data) => {
      this.user = data; // or data.routes if it's nested
    });
  }

  userEmail() {
    this.email = this.accountService.getUserEmail();
  }

  login() {
    this.loginError = '';
    this.isLoading = true;
    this.accountService.get2FAStatus(this.model.email).subscribe({
      next: (status) => {
        if (status.twoFactorStatus) {
          this.showTwoFactorInput = true; // Show input for the 2FA code

          // Send 2FA code to user's email
          this.accountService.sendResetCode(this.model.email).subscribe({
            next: () => {
              console.log('2FA code sent successfully');
              this.isLoading = false;
            },
            error: (error) => {
              console.error('Error sending 2FA code:', error);
              this.loginError =
                'Failed to send the 2FA code. Please try again.';
            },
          });
        } else {
          // Log in directly if 2FA is not enabled
          this.performLogin();
        }
      },
      error: (error) => {
        console.error('Error checking 2FA status:', error);
        this.loginError =
          'An error occurred while checking 2FA status. Please try again.';
      },
    });
  }
  verifyTwoFactorCode(code: string) {
    this.accountService.verifyCode(this.model.email, code).subscribe({
      next: (response) => {
        console.log('2FA verification successful:', response);
        this.twoFactorCode = '';
        this.performLogin();
      },
      error: (error) => {
        console.error('Error verifying 2FA code:', error);
        this.loginError = 'Invalid 2FA code. Please try again.';
        this.invalidCode = true;
        this.twoFactorCode = '';
        this.isLoading = false;
      },
    });
  }
  closePopup() {
    this.invalidCode = false;
  }

  private performLogin() {
    this.accountService.login(this.model).subscribe({
      next: (_) => {
        this.userEmail();
        this.loadName();
        this.router.navigateByUrl('/home');
        this.isLoading = false;
        this.showTwoFactorInput = false;
      },
      error: (error) => {
        console.error('Login error:', error);
        this.loginError = error.message;
        this.isLoading = false;
      },
    });
  }
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  getNotificationIcon(type: string): string {
    switch (type.toLowerCase()) {
      case 'warning':
        return 'bi bi-exclamation-circle-fill text-warning';
      case 'error':
        return 'bi bi-exclamation-triangle-fill text-danger';
      case 'general':
        return 'bi bi-info-circle-fill text-info';
      default:
        return 'bi bi-bell-fill text-secondary';
    }
  }
  showDetails(group: GroupedNotifications): void {
    if (!this.currentlyVisibleNotification) {
      group.showDetails = true;
    }
  }

  hideDetails(group: GroupedNotifications): void {
    if (!this.currentlyVisibleNotification) {
      group.showDetails = false;
    }
  }

  toggleDetails(group: GroupedNotifications): void {
    if (this.currentlyVisibleNotification === group) {
      group.showDetails = !group.showDetails;
      if (!group.showDetails) {
        this.currentlyVisibleNotification = null;
      }
    } else {
      if (this.currentlyVisibleNotification) {
        this.currentlyVisibleNotification.showDetails = false;
      }
      group.showDetails = true;
      this.currentlyVisibleNotification = group;
    }
  }
}
