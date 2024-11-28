import { HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AccountService } from './_services/account.service';
import { NavComponent } from './nav/nav.component';
import { AuthInterceptor } from './_services/auth-interceptor.service';



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavComponent, RouterOutlet],

  providers: [
],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  styles: []
})
export class AppComponent {
  http = inject(HttpClient);
  private accountService = inject(AccountService);
  title = 'GPS';
  users: any;

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }



}