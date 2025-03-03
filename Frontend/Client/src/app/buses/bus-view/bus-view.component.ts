import { Component, inject } from '@angular/core';
import { BusService } from '../../_services/bus.service';
import { Bus } from '../../_models/bus';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { BusEditComponent } from '../bus-edit/bus-edit.component';
import { Title } from '@angular/platform-browser';
import { firstValueFrom } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-bus-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule],
  templateUrl: './bus-view.component.html',
  styleUrl: './bus-view.component.css',
})
export class BusViewComponent {
  private busService = inject(BusService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  private snackBar= inject(MatSnackBar);

  buses: Bus[] = [];

  async ngOnInit() {
    this.titleService.setTitle('Buses');
    await this.loadBuses();
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd && event.url === '/buses') {
        await this.loadBuses();
      }
    });
  }
  async loadBuses() {
    try {
      this.buses = await firstValueFrom(this.busService.getAllBuses());
    }
    catch (err) {
      console.error(err);
    }

    // this.busService.getAllBuses().subscribe((data) => {
    //   this.buses = data; // or data.buss if it's nested
    //   console.log(this.buses)
    // });
  }
  deleteBus(id: number) {
    if (confirm('Are you sure you want to delete this bus?')) {
      this.busService.deleteBus(id).subscribe({
        next: async (response) => {
          await this.loadBuses();
          this.snackBar.open('Bus deleted sucessfully', 'Ok', {
            duration: 4000, // Keep it visible for 4 seconds
            verticalPosition: 'top', // Show at the top
            horizontalPosition: 'center' // Centered horizontally
          });
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting bus', error);
        },
      });
    }
  }
  openEditDialog(bus: Bus) {
    const dialogRef = this.dialog.open(BusEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: bus.id,
        registrationNumber: bus.registrationNumber,
        manufacturer: bus.manufacturer,
        model: bus.model,
        capacity: bus.capacity,
        manufactureYear: bus.manufactureYear,
      }, // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe(async (result) => {
      await this.loadBuses();
      if (result) {
        this.snackBar.open('Bus updated sucessfully', 'Ok', {
          duration: 4000, // Keep it visible for 4 seconds
          verticalPosition: 'top', // Show at the top
          horizontalPosition: 'center' // Centered horizontally
        });
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/buses']);
  }
}
