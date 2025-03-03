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
import { catchError, firstValueFrom, forkJoin, map, Observable, of } from 'rxjs';
import { CommonModule } from '@angular/common';

interface LineWithStationCount extends Line {
  stationCount: number;
}

@Component({
  selector: 'app-route-view',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, CommonModule],
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

      const data = await firstValueFrom(this.lineService.getAllLines());
      this.lines = data; 
  
      const stationCountObservables = this.lines.map(line =>
        this.routeService.getStationCountByLineId(line.id).pipe(
          map(response => ({ ...line, stationCount: response.count }))
        )
      );
  
      this.linesWithStationCount = await firstValueFrom(forkJoin(stationCountObservables));
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
