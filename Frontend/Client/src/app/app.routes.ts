import { Routes } from '@angular/router';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { NewsComponent } from './news/news.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { GalleryComponent } from './gallery/gallery.component';
import { BuyTicketComponent } from './buy-ticket/buy-ticket.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { HomeComponent } from './home/home.component';
import { LineViewComponent } from './lines/line-view/line-view.component';
import { ManagerDashboardComponent } from './manager-dashboard/manager-dashboard.component';
import { RoleGuard } from './guards/role.guard';
import { DriverDashboardComponent } from './driver-dashboard/driver-dashboard.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { NotificationViewComponent } from './notifications/notification-view/notification-view.component';
import { NotificationCreateComponent } from './notifications/notification-create/notification-create.component';
import { LineCreateComponent } from './lines/line-create/line-create.component';
//import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { RegisterComponent } from './register/register.component';
import { BusViewComponent } from './buses/bus-view/bus-view.component';
import { BusCreateComponent } from './buses/bus-create/bus-create.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'news', component: NewsComponent },
  { path: 'schedule', component: ScheduleComponent },
  { path: 'gallery', component: GalleryComponent },
  { path: 'buy-ticket', component: BuyTicketComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'home', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  //{ path: 'lines', component: LineViewComponent },
  //{ path: 'lines/add', component: LineCreateComponent },
  { path: 'notifications', component: NotificationViewComponent },
  { path: 'notifications/add', component: NotificationCreateComponent },
  {
    path: 'manager-dashboard',
    component: ManagerDashboardComponent,
    canActivate: [RoleGuard],
    data: { role: 'Manager' }, // Restrict access to Managers
    children: [
      { path: 'lines', component: LineViewComponent },
      { path: 'lines/add', component: LineCreateComponent },
      { path: 'buses', component: BusViewComponent },
      { path: 'buses/add', component: BusCreateComponent}
    ],
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

