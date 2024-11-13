import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component'; // Import your landing page component

const routes: Routes = [
  // Define routes here
  { path: '', component: LandingPageComponent }, // Default route
  // You can add other routes as needed, for example:
  // { path: 'about', component: AboutComponent },
  // { path: 'contact', component: ContactComponent },
  // { path: '**', redirectTo: '', pathMatch: 'full' } // Wildcard for invalid URLs
];

@NgModule({
  imports: [RouterModule.forRoot(routes)], // Configure the routes
  exports: [RouterModule] // Make the RouterModule available throughout the app
})
export class AppRoutingModule { }
