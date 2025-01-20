import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-manager-dashboard',
  standalone: true,
  imports: [RouterOutlet, RouterLink,CommonModule],
  templateUrl: './manager-dashboard.component.html',
  styleUrl: './manager-dashboard.component.css'
})
export class ManagerDashboardComponent{
  private titleService = inject(Title);
  isSidebarCollapsed = false;
  expandedMenus: { [key: string]: boolean } = {
    manageData: false,
    analytics: false
  };
  ngOnInit()
  {
    this.titleService.setTitle("Manager dashboard");
    //this.setupSidebarToggle();
  }
  toggleSidebar() {
    this.isSidebarCollapsed = !this.isSidebarCollapsed;
    if (this.isSidebarCollapsed) {
      // Close all expanded menus when sidebar is collapsed
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
