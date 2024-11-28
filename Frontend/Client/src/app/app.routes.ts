import { Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BusListComponent } from './bus-list/bus-list.component';
import { BusFormComponent } from './bus-form/bus-form.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { LineViewComponent } from './lines/line-view/line-view.component';
import { LineCreateComponent } from './lines/line-create/line-create.component';


export const routes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'buses', component: BusListComponent },
  { path: 'add', component: BusFormComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'lines', component: LineViewComponent },
  { path: 'lines/add', component: LineCreateComponent },
  
  
  
  
  
  
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
