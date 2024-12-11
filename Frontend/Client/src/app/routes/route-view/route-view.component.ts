import { Component, inject } from '@angular/core';
import { RouteService } from '../../_services/route.service';
import { Route } from '../../_models/route';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { RouteEditComponent } from '../route-edit/route-edit.component';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-route-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule],
  templateUrl: './route-view.component.html',
  styleUrl: './route-view.component.css',
})
export class RouteViewComponent {
  private routeService = inject(RouteService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  routes: Route[] = [];

  ngOnInit() {
    this.titleService.setTitle('Routes');
    this.loadRoutes();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/routes') {
        this.loadRoutes();
      }
    });
  }
  loadRoutes() {
    this.routeService.getAllRoutes().subscribe((data) => {
      this.routes = data; // or data.routes if it's nested
      console.log(this.routes);
    });
  }
  deleteRoute(id: number) {
    if (confirm('Are you sure you want to delete this route?')) {
      this.routeService.deleteRoute(id).subscribe({
        next: (response) => {
          this.loadRoutes();
          console.log('Route deleted successfully', response);
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting route', error);
        },
      });
    }
  }
  openEditDialog(route: Route) {
    const dialogRef = this.dialog.open(RouteEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: route.id,
        lineId: route.lineId,
        stationId: route.stationId,
        distanceFromTheNextStation: route.distanceFromTheNextStation,
      }, // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe((result) => {
      this.loadRoutes();
      if (result) {
        console.log('Updated route:', result);
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/routes']);
  }
}
