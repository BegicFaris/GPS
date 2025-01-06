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
import { RouteViewComponent } from './routes/route-view/route-view.component';
import { RouteCreateComponent } from './routes/route-create/route-create.component';
import { ManagerViewComponent } from './managers/manager-view/manager-view.component';
import { ManagerCreateComponent } from './managers/manager-create/manager-create.component';
import { DriverViewComponent } from './drivers/driver-view/driver-view.component';
import { DriverCreateComponent } from './drivers/driver-create/driver-create.component';
import { PassengerViewComponent } from './passengers/passenger-view/passenger-view.component';
import { CreditCardViewComponent } from './creditCards/creditCard-view/creditCard-view.component';
import { CreditCardCreateComponent } from './creditCards/creditCard-create/creditCard-create.component';
import { StationViewComponent } from './stations/station-view/station-view.component';
import { StationEditComponent } from './stations/station-edit/station-edit.component';
import { StationCreateComponent } from './stations/station-create/station-create.component';
import { ScheduleViewComponent } from './scheduleCRUD/schedule-view/schedule-view.component';
import { ScheduleCreateComponent } from './scheduleCRUD/schedule-create/schedule-create.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { FaqsComponent } from './FAQs/faqs.component';
import { ShiftCreateComponent } from './shifts/shift-create/shift-create.component';
import { ShiftViewComponent } from './shifts/shift-view/shift-view.component';
import { ShiftDetailsComponent } from './shifts/shift-details/shift-details.component';

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
  { path: 'news', component: NewsComponent },
  { path: 'profile', component: UserProfileComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'faqs', component: FaqsComponent },
  //{ path: 'lines', component: LineViewComponent },
  //{ path: 'lines/add', component: LineCreateComponent },
  { path: 'gallery', component: GalleryComponent },

  
  {
    path: 'manager-dashboard',
    component: ManagerDashboardComponent,
    canActivate: [RoleGuard],
    data: { role: 'Manager' }, // Restrict access to Managers
    children: [
      { path: 'lines', component: LineViewComponent },
      { path: 'lines/add', component: LineCreateComponent },
      { path: 'buses', component: BusViewComponent },
      { path: 'buses/add', component: BusCreateComponent }, 
      { path: 'routes', component: RouteViewComponent },
      { path: 'routes/add', component: RouteCreateComponent },
      { path: 'managers', component: ManagerViewComponent },
      { path: 'managers/add', component: ManagerCreateComponent },
      { path: 'drivers', component: DriverViewComponent },
      { path: 'drivers/add', component: DriverCreateComponent },
      { path: 'passengers', component: PassengerViewComponent },
      { path: 'creditCards', component: CreditCardViewComponent },
      { path: 'creditCards/add', component: CreditCardCreateComponent },
      { path: 'notifications', component: NotificationViewComponent },
      { path: 'notifications/add', component: NotificationCreateComponent },
      { path: 'stations', component: StationViewComponent },
      { path: 'stations/add', component:  StationCreateComponent},
      { path: 'schedules', component: ScheduleViewComponent },
      { path: 'schedules/add', component:  ScheduleCreateComponent},
      { path: 'shifts/add', component:  ShiftCreateComponent},
      { path: 'shifts', component:  ShiftViewComponent},
      { path: 'shifts/details', component:  ShiftDetailsComponent}
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

