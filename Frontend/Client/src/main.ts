// src/main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { importProvidersFrom } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BusListComponent } from './app/bus-list/bus-list.component';
import { BusFormComponent } from './app/bus-form/bus-form.component';
import { LandingPageComponent } from './app/landing-page/landing-page.component';

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(
      HttpClientModule, 
      RouterModule.forRoot([
        { path: 'landing-page', component: LandingPageComponent },
        { path: '', component: BusListComponent },
        { path: 'add', component: BusFormComponent },
        { path: 'edit/:id', component: BusFormComponent },
      ])
    ),
  ],
})
  .catch((err) => console.error(err));
