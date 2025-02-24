
import { Component, inject, OnInit } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';
import { AccountService } from './_services/account.service';
import { NavComponent } from './nav/nav.component';
import { AuthInterceptor } from './_services/auth-interceptor.service';
import { FooterComponent } from "./footer/footer.component";
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { GalleryComponent } from './gallery/gallery.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { NewsComponent } from './news/news.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { BuyTicketComponent } from './buy-ticket/buy-ticket.component';
import { TranslateService } from '@ngx-translate/core';
import { TranslateModule } from '@ngx-translate/core';
 

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavComponent, RouterOutlet, FooterComponent, RouterModule, TranslateModule],

  providers: [
],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  styles: []
})
export class AppComponent implements OnInit{
  private accountService = inject(AccountService);

  ngOnInit(): void {
    this.setCurrentUser();
    
    this.translate.setDefaultLang('en');
    this.translate.use('en');
  }

  constructor(private translate: TranslateService) {}
 
 

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

  routes: Routes = [
        { path: 'home', component: HomeComponent },
        { path: 'news', component: NewsComponent },
        { path: 'schedule', component: ScheduleComponent },
        { path: 'gallery', component: GalleryComponent },
        { path: 'tickets', component: BuyTicketComponent },
        { path: 'aboutUs', component: AboutUsComponent },
      ];

}