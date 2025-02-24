import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-driver-dashboard',
  standalone: true,
  imports: [RouterOutlet, RouterLink,CommonModule],
  templateUrl: './driver-dashboard.component.html',
  styleUrl: './driver-dashboard.component.css'
})
export class DriverDashboardComponent {
  private titleService = inject(Title);
  isSidebarCollapsed = false;
  expandedMenus: { [key: string]: boolean } = {
    manageData: false,
    analytics: false
  };
  ngOnInit()
  {
    this.titleService.setTitle("Driver dashboard");
  }
  toggleSidebar() {
    this.isSidebarCollapsed = !this.isSidebarCollapsed;
    if (this.isSidebarCollapsed) {
      Object.keys(this.expandedMenus).forEach(key => {
        this.expandedMenus[key] = false;
      });
    }
  }
  toggleMenu(menuKey: string) {
    if (!this.isSidebarCollapsed) {
      this.expandedMenus[menuKey] = !this.expandedMenus[menuKey];
    }
  }

}
