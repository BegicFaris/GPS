import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouteService } from '../../_services/route.service';
import { StationService } from '../../_services/station.service';
import { Router } from '@angular/router';
import { Station } from '../../_models/station';
import { Title } from '@angular/platform-browser';
import { Line } from '../../_models/line';
import { LineService } from '../../_services/line.service';

@Component({
  selector: 'app-route-create',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './route-create.component.html',
  styleUrl: './route-create.component.css',
})
export class RouteCreateComponent {
  private router = inject(Router);
  private routeService = inject(RouteService);
  private stationService = inject(StationService);
  private lineService = inject(LineService);
  private titleService = inject(Title);
  stations: Station[] = [];

  lines: Line[] = [];


  ngOnInit() {
    this.titleService.setTitle('Add route');
    this.loadStations();
    this.loadLines();
  }
  routeCreate: any = {};
  addNewRoute(newRouteForm: NgForm) {

    if (newRouteForm.valid) {
      if (this.routeCreate.isActive === undefined) {
        this.routeCreate.isActive = false;
      }
      this.routeService.createRoute(this.routeCreate).subscribe({
        next: (response) => {
          console.log(response);
          this.cancel();
        },
      });
      this.router.navigate(['/manager-dashboard/routes']);
    }
    else{
      Object.keys(newRouteForm.controls).forEach(field => {
        const control = newRouteForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }
  cancel() {
    this.router.navigate(['/manager-dashboard/routes']);
  }
  loadStations() {
    this.stationService.getAllStations().subscribe((data) => {
      this.stations = data; // or data.routes if it's nested
    });
  }
  loadLines(){
    this.lineService.getAllLines().subscribe((data) => {
      this.lines = data;
    })
  }
}
