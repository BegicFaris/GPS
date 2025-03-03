import { Routes } from '@angular/router';
import { RoleGuard } from './guards/role.guard';

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
    loadComponent: () => import('./news/news.component').then(m => m.NewsComponent),
    canActivate: [RoleGuard],
    data: { role: 'Any' }
  },
  {
    path: 'schedule',
    loadComponent: () => import('./schedule/schedule.component').then(m => m.ScheduleComponent),
    canActivate: [RoleGuard],
    data: { role: 'Any' }
  },
  {
    path: 'gallery',
    loadComponent: () => import('./gallery/gallery.component').then(m => m.GalleryComponent),
    canActivate: [RoleGuard],
    data: { role: 'Any' }
  },
  {
    path: 'buy-ticket',
    loadComponent: () => import('./buy-ticket/buy-ticket.component').then(m => m.BuyTicketComponent),
    canActivate: [RoleGuard],
    data: { role: 'Any' }
  },
  {
    path: 'about-us',
    loadComponent: () => import('./about-us/about-us.component').then(m => m.AboutUsComponent),
    canActivate: [RoleGuard],
    data: { role: 'Any' }
  },
  {
    path: 'profile',
    loadComponent: () => import('./user-profile/user-profile.component').then(m => m.UserProfileComponent),
    canActivate: [RoleGuard],
    data: { role: 'Any' }
  },
  {
    path: 'register',
    loadComponent: () => import('./register/register.component').then(m => m.RegisterComponent)
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
        path: 'routes/details',
        loadComponent: () => import('./routes/route-details/route-details.component').then(m => m.RouteDetailsComponent)
      },
      {
        path: 'drivers/add',
        loadComponent: () => import('./drivers/driver-create/driver-create.component').then(m => m.DriverCreateComponent)
      },
      {
        path: 'drivers',
        loadComponent: () => import('./drivers/driver-view/driver-view.component').then(m => m.DriverViewComponent)
      },
      {
        path: 'shifts/add',
        loadComponent: () => import('./shifts/shift-create/shift-create.component').then(m => m.ShiftCreateComponent)
      },
      {
        path: 'shifts',
        loadComponent: () => import('./shifts/shift-view/shift-view.component').then(m => m.ShiftViewComponent)
      },
      {
        path: 'shifts/details',
        loadComponent: () => import('./shifts/shift-details/shift-details.component').then(m => m.ShiftDetailsComponent)
      },
      {
        path: 'notifications/add',
        loadComponent: () => import('./notifications/notification-create/notification-create.component').then(m => m.NotificationCreateComponent)
      },
      {
        path: 'notifications',
        loadComponent: () => import('./notifications/notification-view/notification-view.component').then(m => m.NotificationViewComponent)
      },
      {
        path: 'managers/add',
        loadComponent: () => import('./managers/manager-create/manager-create.component').then(m => m.ManagerCreateComponent)
      },
      {
        path: 'managers',
        loadComponent: () => import('./managers/manager-view/manager-view.component').then(m => m.ManagerViewComponent)
      },
      {
        path: 'passengers',
        loadComponent: () => import('./passengers/passenger-view/passenger-view.component').then(m => m.PassengerViewComponent)
      },
      {
        path: 'schedules/add',
        loadComponent: () => import('./schedules/schedule-create/schedule-create.component').then(m => m.ScheduleCreateComponent)
      },
      {
        path: 'schedules',
        loadComponent: () => import('./schedules/schedule-view/schedule-view.component').then(m => m.ScheduleViewComponent)
      },
      {
        path: 'stations/add',
        loadComponent: () => import('./stations/station-create/station-create.component').then(m => m.StationCreateComponent)
      },
      {
        path: 'stations',
        loadComponent: () => import('./stations/station-view/station-view.component').then(m => m.StationViewComponent)
      },
      {
        path: 'ticket-graph',
        loadComponent: () => import('./ticket-graph/ticket-graph.component').then(m => m.TicketGraphComponent)
      },
    ],
  },
  {
    path: 'driver-dashboard',
    loadComponent: () => import('./driver-dashboard/driver-dashboard.component').then(m => m.DriverDashboardComponent),
    canActivate: [RoleGuard],
    data: { role: 'Driver' },
    children: [
      {
        path: 'shift-infos',
        loadComponent: () => import('./driver-dashboard/shift-info/shift-info.component').then(m => m.ShiftInfoComponent)
      },
      {
        path: 'shift-lists',
        loadComponent: () => import('./driver-dashboard/shift-list/shift-list.component').then(m => m.ShiftListComponent)
      },
      {
        path: 'shift-overviews/:id',
        loadComponent: () => import('./driver-dashboard/shift-overview/shift-overview.component').then(m => m.ShiftOverviewComponent)
      },

     ]
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
