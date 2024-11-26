// src/main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { importProvidersFrom } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BusListComponent } from './app/bus/bus-list/bus-list.component';
import { BusFormComponent } from './app/bus/bus-form/bus-form.component';
import { LandingPageComponent } from './app/landing-page/landing-page.component';
import { LineListComponent } from './app/line/line-list/line-list.component';
import { LineFormComponent } from './app/line/line-form/line-form.component';

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(
      HttpClientModule, 
      RouterModule.forRoot([
        { path: 'landing-page', component: LandingPageComponent },
        { path: 'buses', component: BusListComponent },
        { path: 'buses/add', component: BusFormComponent },
        { path: 'lines', component: LineListComponent },
        { path: 'lines/add', component: LineFormComponent },
      ])
    ),
  ],
})
  .catch((err) => console.error(err));
