import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { MyAppUserService } from '../_services/my-app-user.service';
import { MyAppUser } from '../_models/my-app-user';

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
export class NavComponent implements OnInit{
  ngOnInit(): void {
    this.userEmail();  // Get email after login
        this.loadName();
  }
  
  showPassword: boolean = false;
  accountService = inject(AccountService);
  myAppUserService = inject(MyAppUserService);
  private router = inject(Router);
  model: any = {};
  user: any={};
  email: string ="";
  loginError: string = '';
  isLoading: boolean = false;
  
  get userRole(): string | null{
    return this.accountService.getUserRole();
    
  }
  
  loadName() {
    this.myAppUserService.getMyAppUserByEmail(this.email).subscribe(
      (data) => {
        this.user = data; // or data.routes if it's nested
      },
    );
  }

  userEmail(){
    this.email = this.accountService.getUserEmail();
  }
  
  login() {
    this.loginError = '';
    this.isLoading = true;
    this.accountService.login(this.model).subscribe({
      next: (_) => {
        this.userEmail();  // Get email after login
        this.loadName();
        this.router.navigateByUrl('/home');
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Login error:', error);
        this.loginError = error.message;
        alert(this.loginError);
        this.isLoading = false;
      }
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
