import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-driver-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './driver-dashboard.component.html',
  styleUrl: './driver-dashboard.component.css'
})
export class DriverDashboardComponent {
  private titleService = inject(Title);

  ngOnInit()
  {
    this.titleService.setTitle("Driver dashboard");
  }
}
