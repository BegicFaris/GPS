import { Routes } from '@angular/router';
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
import { LineViewComponent } from './lines/line-view/line-view.component';

export const routes: Routes = [

  { path: '',
    redirectTo: '/home',
    pathMatch: 'full' 
  },
  { path: 'home', 
    loadComponent: () => import('./home/home.component').then(m => m.HomeComponent) 
  },
  {
    path: 'forgot-password',
    loadComponent: () => import('./forgot-password/forgot-password.component').then(m => m.ForgotPasswordComponent)
  },
  {
    path: 'reset-password',
    loadComponent: () => import('./reset-password/reset-password.component').then(m => m.ResetPasswordComponent)
  },
  {
    path: 'news',
    loadComponent: () => import('./news/news.component').then(m => m.NewsComponent)
  },
  {
    path: 'schedule',
    loadComponent: () => import('./schedule/schedule.component').then(m => m.ScheduleComponent)
  },
  {
    path: 'gallery',
    loadComponent: () => import('./gallery/gallery.component').then(m => m.GalleryComponent)
  },
  {
    path: 'buy-ticket',
    loadComponent: () => import('./buy-ticket/buy-ticket.component').then(m => m.BuyTicketComponent)
  },
  {
    path: 'about-us',
    loadComponent: () => import('./about-us/about-us.component').then(m => m.AboutUsComponent)
  },
  {
    path: 'profile',
    loadComponent: () => import('./user-profile/user-profile.component').then(m => m.UserProfileComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./register/register.component').then(m => m.RegisterComponent)
  },
  {
    path: 'faqs',
    loadComponent: () => import('./FAQs/faqs.component').then(m => m.FaqsComponent)
  },
  {
    path: 'manager-dashboard',
    loadComponent: () => import('./manager-dashboard/manager-dashboard.component').then(m => m.ManagerDashboardComponent),
    canActivate: [RoleGuard],
    data: { role: 'Manager' },
    children: [
      {
        path: 'lines',
        loadComponent: () => import('./lines/line-view/line-view.component').then(m => m.LineViewComponent)
      },
      {
        path: 'lines/add',
        loadComponent: () => import('./lines/line-create/line-create.component').then(m => m.LineCreateComponent)
      },
      {
        path: 'discounts',
        loadComponent: () => import('./discounts/discount-view/discount-view.component').then(m => m.DiscountViewComponent)
      },
      {
        path: 'discounts/add',
        loadComponent: () => import('./discounts/discount-create/discount-create.component').then(m => m.DiscountCreateComponent)
      },
      {
        path: 'buses',
        loadComponent: () => import('./buses/bus-view/bus-view.component').then(m => m.BusViewComponent)
      },
      {
        path: 'buses/add',
        loadComponent: () => import('./buses/bus-create/bus-create.component').then(m => m.BusCreateComponent)
      },
      {
        path: 'routes',
        loadComponent: () => import('./routes/route-view/route-view.component').then(m => m.RouteViewComponent)
      },
      {
        path: 'routes/add',
        loadComponent: () => import('./routes/route-create/route-create.component').then(m => m.RouteCreateComponent)
      },
      // Add other children with loadComponent similarly...
    ],
  },
  {
    path: 'driver-dashboard',
    loadComponent: () => import('./driver-dashboard/driver-dashboard.component').then(m => m.DriverDashboardComponent),
    canActivate: [RoleGuard],
    data: { role: 'Driver' },
  },
  {
    path: 'unauthorized',
    loadComponent: () => import('./unauthorized/unauthorized.component').then(m => m.UnauthorizedComponent),
  },
  {
    path: '**',
    redirectTo: '/unauthorized',
    pathMatch: 'full'
  },
];
