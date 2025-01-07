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
