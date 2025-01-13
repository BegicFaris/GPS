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
import { catchError, Observable, of } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-route-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule, CommonModule],
  templateUrl: './route-view.component.html',
  styleUrl: './route-view.component.css',
})
export class RouteViewComponent {
  private lineService = inject(LineService);
  private routeService = inject(RouteService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  lines: Line[] = [];

  ngOnInit() {
    this.titleService.setTitle('Routes');
    this.loadLines();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/routes') {
        this.loadLines();
      }
    });
  }
  loadLines() {
    this.lineService.getAllLines().subscribe((data) => {
      this.lines = data; // or data.routes if it's nested
      console.log(this.lines);
    });
  }

  getStationCount(lineId: number): Observable<number> {
    return this.routeService.getStationCountByLineId(lineId).pipe(catchError((error) => {
      console.error('Error fetching station count:', error); return of(0); 
     }) ); }


      openRouteDetails(line: Line){
        this.router.navigate(['manager-dashboard/routes/details'], { queryParams: line });
      }
      cancel() {
        this.router.navigate(['manager-dashboard/routes']);
      }
    }
