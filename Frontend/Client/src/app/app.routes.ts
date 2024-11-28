import { Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BusListComponent } from './bus/bus-list/bus-list.component';
import { BusFormComponent } from './bus/bus-form/bus-form.component';
import { LineListComponent } from './line/line-list/line-list.component';
import { LineFormComponent } from './line/line-form/line-form.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';

export const routes: Routes = [
  { path: 'buses', component: BusListComponent },
  { path: 'buses/add', component: BusFormComponent },
  { path: 'lines', component: LineListComponent },
  { path: 'lines/add', component: LineFormComponent },
  { path: '', component: LandingPageComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

