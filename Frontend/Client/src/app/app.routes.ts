import { Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BusListComponent } from './bus-list/bus-list.component';
import { BusFormComponent } from './bus-form/bus-form.component';
import { LoginComponent } from '../app/login/login.component';

export const routes: Routes = [
    {path: 'landing-page', component: LandingPageComponent},
    { path: 'buses', component: BusListComponent },
    { path: 'add', component: BusFormComponent },
    { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];


