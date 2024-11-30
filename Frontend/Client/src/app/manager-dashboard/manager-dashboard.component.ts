import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-manager-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './manager-dashboard.component.html',
  styleUrl: './manager-dashboard.component.css'
})
export class ManagerDashboardComponent {
  private titleService = inject(Title);

  ngOnInit()
  {
    this.titleService.setTitle("Manager dashboard");
  }


}
