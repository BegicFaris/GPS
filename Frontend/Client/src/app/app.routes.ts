import { Routes } from '@angular/router';
<<<<<<< HEAD
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BusListComponent } from './bus-list/bus-list.component';
import { BusFormComponent } from './bus-form/bus-form.component';

export const routes: Routes = [
    {path: 'landing-page', component: LandingPageComponent},
    { path: '', component: BusListComponent },
    { path: 'add', component: BusFormComponent },
];
=======
import { LoginComponent } from '../app/login/login.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];
>>>>>>> 56751be0374cb49dd7a70f15e10cb43d70c7e40e
