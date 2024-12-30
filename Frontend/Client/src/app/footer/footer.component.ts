import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { GalleryComponent } from '../gallery/gallery.component';
import { FaqsComponent } from '../FAQs/faqs.component';
import { NewsComponent } from '../news/news.component';
import { ScheduleComponent } from '../schedule/schedule.component';
import { BuyTicketComponent } from '../buy-ticket/buy-ticket.component';
import { AboutUsComponent } from '../about-us/about-us.component';

@Component({
    selector: 'app-footer',
    standalone: true,
    imports: [RouterModule],
    templateUrl: './footer.component.html',
    styleUrl: './footer.component.css'
  })
  export class FooterComponent {
    private titleService = inject(Title);
  
  
  
    ngOnInit() {
      this.titleService.setTitle("Footer");
  
    }
  
    routes: Routes = [
      { path: 'home', component: HomeComponent },
      { path: 'news', component: NewsComponent },
      { path: 'schedule', component: ScheduleComponent },
      { path: 'gallery', component: GalleryComponent },
      { path: 'tickets', component: BuyTicketComponent },
      { path: 'aboutUs', component: AboutUsComponent },
      { path: 'faqs', component: FaqsComponent },
    ];
  }
  