import { Component, inject } from '@angular/core';
import { LineService } from '../../_services/line.service';
import { Line } from '../../_models/line';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { StationEditComponent } from '../station-edit/station-edit.component';
import { Station } from '../../_models/station';
import { Title } from '@angular/platform-browser';
import { ManagerService } from '../../_services/manager.service';
import { CommonModule } from '@angular/common';
import { StationService } from '../../_services/station.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-station-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule, CommonModule],
  templateUrl: './station-view.component.html',
  styleUrl: './station-view.component.css',
})
export class StationViewComponent {
  private stationService = inject(StationService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  private snackBar = inject(MatSnackBar);
  stations: Station[] = [];

 async  ngOnInit() {
    this.titleService.setTitle('Station');
    await this.loadStations();
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd && event.url === '/stations') {
        await this.loadStations();
      }
    });
  }
  async loadStations() {
    try {
      this.stations = await firstValueFrom(this.stationService.getAllStations());
    }
    catch (err) {
      console.error(err);
    }

  }
  deleteStation(id: number) {
    if (confirm('Are you sure you want to delete this station?')) {
      this.stationService.deleteStation(id).subscribe({
        next: async () => {
          await this.loadStations();
          this.snackBar.open('Station deleted sucessfully', 'Ok', {
            duration: 4000, // Keep it visible for 4 seconds
            verticalPosition: 'top', // Show at the top
            horizontalPosition: 'center' // Centered horizontally
          });
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          this.snackBar.open(error, 'Close', {
            panelClass: ['.error-snackbar'],
            duration: 3000, // Duration in milliseconds
            horizontalPosition: 'center', // Can be 'start', 'center', 'end', 'left', 'right'
            verticalPosition: 'bottom', // Can be 'top' or 'bottom'
          });
        },
      });
    }
  }
  openEditDialog(station: Station) {
    const dialogRef = this.dialog.open(StationEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: station.id,
        name: station.name,
        location: station.location,
        gpsCode: station.gpsCode,
        zoneId: station.zoneId,
      }, 
    });
    dialogRef.afterClosed().subscribe(async (result) => {
      await this.loadStations();
      if (result) {
        this.snackBar.open('Station updated sucessfully', 'Ok', {
          duration: 4000,
          verticalPosition: 'top', 
          horizontalPosition: 'center' 
        });
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/stations']);
  }
}
