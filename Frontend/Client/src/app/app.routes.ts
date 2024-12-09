import { Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BusListComponent } from './bus/bus-list/bus-list.component';
import { BusFormComponent } from './bus/bus-form/bus-form.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { NewsComponent } from './news/news.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { GalleryComponent } from './gallery/gallery.component';
import { BuyTicketComponent } from './buy-ticket/buy-ticket.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { HomeComponent } from './home/home.component';
import { LineViewComponent } from './lines/line-view/line-view.component';
import { LineCreateComponent } from './lines/line-create/line-create.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { RegisterComponent } from './register/register.component';

export const routes: Routes = [
  { path: 'buses', component: BusListComponent },
  { path: 'buses/add', component: BusFormComponent },
  { path: '', component: HomeComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'news', component:NewsComponent},
  { path: 'schedule', component:ScheduleComponent},
  { path: 'gallery', component:GalleryComponent},
  { path: 'buy-ticket', component:BuyTicketComponent},
  { path: 'about-us', component:AboutUsComponent},
  { path: 'home', component:HomeComponent},
  { path: 'lines', component: LineViewComponent },
  { path: 'lines/add', component: LineCreateComponent },
  { path: 'register', component: RegisterComponent },
  
  


  
  
  
  //ne diraj ovo
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

