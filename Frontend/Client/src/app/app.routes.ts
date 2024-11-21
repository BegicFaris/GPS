import { Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BusListComponent } from './bus-list/bus-list.component';
import { BusFormComponent } from './bus-form/bus-form.component';

export const routes: Routes = [
    {path: 'landing-page', component: LandingPageComponent},
    { path: '', component: BusListComponent },
    { path: 'add', component: BusFormComponent },
];
