import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLink, Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { GalleryComponent } from '../gallery/gallery.component';
import { AboutUsComponent } from '../about-us/about-us.component';
import { FaqsComponent } from '../FAQs/faqs.component';
import { NewsComponent } from '../news/news.component';
import { ScheduleComponent } from '../schedule/schedule.component';

@Component({
  selector: 'app-buy-ticket',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './buy-ticket.component.html',
  styleUrl: './buy-ticket.component.css'
})
export class BuyTicketComponent {
  private titleService = inject(Title);

  ngOnInit() {
    this.titleService.setTitle("Buy ticket");
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
