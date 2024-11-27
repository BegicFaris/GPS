import { Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BusListComponent } from './bus/bus-list/bus-list.component';
import { BusFormComponent } from './bus/bus-form/bus-form.component';
import { LineListComponent } from './line/line-list/line-list.component';
import { LineFormComponent } from './line/line-form/line-form.component';

export const routes: Routes = [
  { path: 'landing-page', component: LandingPageComponent },
  { path: 'buses', component: BusListComponent },
  { path: 'buses/add', component: BusFormComponent },
  { path: 'lines', component: LineListComponent },
  { path: 'lines/add', component: LineFormComponent },
];

