import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AccountService } from './_services/account.service';
import { NavComponent } from './nav/nav.component';
import { LandingPageComponent } from "./landing-page/landing-page.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavComponent, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  styles: []
})
export class AppComponent implements OnInit{
  private accountService = inject(AccountService);

  ngOnInit(): void{
    this.setCurrentUser();
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

}