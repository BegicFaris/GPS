import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLink, Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { GalleryComponent } from '../gallery/gallery.component';
import { BuyTicketComponent } from '../buy-ticket/buy-ticket.component';
import { AboutUsComponent } from '../about-us/about-us.component';
import { FaqsComponent } from '../FAQs/faqs.component';
import { NewsComponent } from '../news/news.component';

@Component({
  selector: 'app-schedule',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './schedule.component.html',
  styleUrl: './schedule.component.css'
})
export class ScheduleComponent {
  private titleService = inject(Title);

  ngOnInit()
  {
    this.titleService.setTitle("Schedule");
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
