import { Component, inject } from '@angular/core';
import { RouteService } from '../../_services/route.service';
import { Route } from '../../_models/route';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { Title } from '@angular/platform-browser';
import { Line } from '../../_models/line';
import { __param } from 'tslib';
import { LineService } from '../../_services/line.service';
import { catchError, firstValueFrom, Observable, of } from 'rxjs';
import { CommonModule } from '@angular/common';

interface LineWithStationCount extends Line {
  stationCount: number;
}

@Component({
  selector: 'app-route-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule, CommonModule],
  templateUrl: './route-view.component.html',
  styleUrl: './route-view.component.css',
})
export class RouteViewComponent {
  linesWithStationCount: LineWithStationCount[] = [];
  private lineService = inject(LineService);
  private routeService = inject(RouteService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  lines: Line[] = [];

  async ngOnInit() {
    this.titleService.setTitle('Routes');
    await this.loadLines();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/routes') {
        this.loadLines();
      }
    });
  }
  async loadLines() {
    try {
      // Fetch all lines
      const data = await firstValueFrom(this.lineService.getAllLines());
      this.lines = data; // Assuming data contains the lines

      // Create a new array of lines with station counts
      for (let line of this.lines) {
        const response = await firstValueFrom(this.routeService.getStationCountByLineId(line.id));
        const lineWithStationCount: LineWithStationCount = {
          ...line, // Copy all properties from the original line
          stationCount: response.count // Add the stationCount property
        };
        this.linesWithStationCount.push(lineWithStationCount); // Add it to a new array
      }

      console.log(this.linesWithStationCount); // Now, linesWithStationCount contains the new objects with stationCount
    } catch (error) {
      console.error('Error loading lines:', error);
    }
  }

  openRouteDetails(line: Line) {
    this.router.navigate(['manager-dashboard/routes/details'], { queryParams: line });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/routes']);
  }
}
