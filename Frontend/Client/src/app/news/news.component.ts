import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLink, Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { ScheduleComponent } from '../schedule/schedule.component';
import { GalleryComponent } from '../gallery/gallery.component';
import { BuyTicketComponent } from '../buy-ticket/buy-ticket.component';
import { AboutUsComponent } from '../about-us/about-us.component';
import { FaqsComponent } from '../FAQs/faqs.component';

@Component({
  selector: 'app-news',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './news.component.html',
  styleUrl: './news.component.css'
})
export class NewsComponent {
  private titleService = inject(Title);

  ngOnInit()
  {
    this.titleService.setTitle("News");
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
