import { Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BusListComponent } from './bus-list/bus-list.component';
import { BusFormComponent } from './bus-form/bus-form.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { NewsComponent } from './news/news.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { GalleryComponent } from './gallery/gallery.component';
import { BuyTicketComponent } from './buy-ticket/buy-ticket.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { HomeComponent } from './home/home.component';
import { LineViewComponent } from './lines/line-view/line-view.component';
import { LineCreateComponent } from './lines/line-create/line-create.component';
import { ManagerDashboardComponent } from './manager-dashboard/manager-dashboard.component';
import { RoleGuard } from './guards/role.guard';
import { DriverDashboardComponent } from './driver-dashboard/driver-dashboard.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

export const routes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'buses', component: BusListComponent },
  { path: 'add', component: BusFormComponent },
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
  {
    path: 'manager-dashboard',
    component: ManagerDashboardComponent,
    canActivate: [RoleGuard],
    data: { role: 'Manager' }, // Restrict access to Managers
  },
  {
    path: 'driver-dashboard',
    component: DriverDashboardComponent,
    canActivate: [RoleGuard],
    data: { role: 'Driver' }, // Restrict access to Drivers
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent, // A simple page showing "Access Denied"
  },
  
  


  
  
  
  //ne diraj ovo
  { path: '**', redirectTo: '/unauthorized', pathMatch: 'full' },
];
