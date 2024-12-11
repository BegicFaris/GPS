import { Component, inject, Inject } from '@angular/core';
import { FormsModule, } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RouteService } from '../../_services/route.service';
import { StationService } from '../../_services/station.service';
import { Station } from '../../_models/station';
import { LineService } from '../../_services/line.service'
import { Line } from '../../_models/line'
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-route-edit',
  standalone: true,
  imports: [FormsModule, ],
  templateUrl: './route-edit.component.html',
  styleUrl: './route-edit.component.css'
})
export class RouteEditComponent {

  constructor(public dialogRef: MatDialogRef<RouteEditComponent>, @Inject(MAT_DIALOG_DATA) public routeUpdate: { id: number, lineId: number, line: Line, stationId: number, station: Station, distanceFromTheNextStation: number }) {

  }
  private routeService = inject(RouteService);
  private stationService = inject(StationService);
  private lineService = inject(LineService);
  private titleService = inject(Title);
  stations: Station[] = [];

  lines: Line[] = [];

  ngOnInit(): void {
    this.titleService.setTitle("Update route");
    this.loadStations();
    this.loadLines();
  }

  saveChanges() {
    this.routeService.updateRoute(this.routeUpdate).subscribe({
      next: response => {
        console.log('route update successfully', response);
      },
      error: error => {
        console.error('Error deleting route', error);
      }
    });
    this.dialogRef.close(this.routeUpdate);
  }
  closeDialog() {
    this.dialogRef.close();
  }
  loadStations() {
    this.stationService.getAllStations().subscribe(
      (data) => {
        this.stations = data; // or data.routes if it's nested
      },
    );
  }
  loadLines() {
    this.lineService.getAllLines().subscribe((data) => {
      this.lines = data;
    })
  }
}

