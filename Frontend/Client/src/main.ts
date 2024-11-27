// src/main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { importProvidersFrom } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BusListComponent } from './app/bus/bus-list/bus-list.component';
import { BusFormComponent } from './app/bus/bus-form/bus-form.component';
import { LineListComponent } from './app/line/line-list/line-list.component';
import { LineFormComponent } from './app/line/line-form/line-form.component';
import { LoginComponent } from './app/login/login.component';
import { LandingPageComponent } from './app/landing-page/landing-page.component';

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


  // bootstrapApplication(AppComponent, {
  //   providers: [
  //     provideHttpClient(),
  //     provideRouter(routes),
  //     provideAnimations()
  //   ],
  // }).catch((err) => console.error(err));