import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module'; // Import AppRoutingModule
import { LandingPageComponent } from './landing-page/landing-page.component'; // Import the landing page component

@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent, // Declare your landing page component
  ],
  imports: [
    BrowserModule,
    AppRoutingModule, // Add the routing module to imports
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
