import { Component } from '@angular/core';
import { NavComponent } from "../nav/nav.component";
import { RouterModule, RouterOutlet } from '@angular/router';
import { RegisterComponent } from "../register/register.component";

@Component({
  standalone:true,
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css'],
  imports: [RegisterComponent],
})
export class LandingPageComponent {
  registerMode = false;

  registerToggle(){
    this.registerMode= !this.registerMode;
  }
}
