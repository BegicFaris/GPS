import { Component, inject, OnInit } from '@angular/core';
import { NavComponent } from '../nav/nav.component';
import { RouterModule, RouterOutlet } from '@angular/router';
import { RegisterComponent } from '../register/register.component';
import { HttpClient } from '@angular/common/http';

@Component({
  standalone: true,
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css'],
  imports: [RegisterComponent],
})
export class LandingPageComponent implements OnInit {
  http = inject(HttpClient);
  registerMode = false;
  users: any;

  ngOnInit(): void {}

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
