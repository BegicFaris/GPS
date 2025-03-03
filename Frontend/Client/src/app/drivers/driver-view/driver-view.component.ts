import { Component, inject } from '@angular/core';
import { LineService } from '../../_services/line.service';
import { Line } from '../../_models/line';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { DriverEditComponent } from '../driver-edit/driver-edit.component';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { DriverService } from '../../_services/driver.service';
import { Driver } from '../../_models/driver';
import { firstValueFrom } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-driver-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule, CommonModule],
  templateUrl: './driver-view.component.html',
  styleUrl: './driver-view.component.css',
})
export class DriverViewComponent {
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  private driverService = inject(DriverService);
  private snackBar= inject(MatSnackBar);

  drivers: Driver[] = [];

  async ngOnInit() {
    this.titleService.setTitle('Drivers');
    await this.loadDrivers();
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd && event.url === '/drivers') {
        await this.loadDrivers();
      }
    });
  }
  async loadDrivers() {
    try {
     this.drivers = await firstValueFrom(this.driverService.getAllDrivers());
    } 
    catch (err) {
      console.error(err);
    }
  }
  deleteDriver(id: number) {
    if (confirm('Are you sure you want to delete this driver?')) {
      this.driverService.deleteDriver(id).subscribe({
        next: async (response) => {
          await this.loadDrivers();
          this.snackBar.open('Driver deleted sucessfully', 'Ok', {
            duration: 4000, // Keep it visible for 4 seconds
            verticalPosition: 'top', // Show at the top
            horizontalPosition: 'center' // Centered horizontally
          });
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting driver', error);
          alert(`Error: ${error.message || 'An error occurred while deleting the driver.'}`);
        },
      });
    }
  }
  openEditDialog(driver: Driver) {
    const dialogRef = this.dialog.open(DriverEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: driver.id,
        firstName: driver.firstName,
        lastName: driver.lastName,
        email: driver.email,
        birthDate: driver.birthDate,
        address: driver.address,
        hireDate: driver.hireDate,
        license: driver.license,
        driversLicenseNumber: driver.driversLicenseNumber,
        workingHoursInAWeek: driver.workingHoursInAWeek,
      }, // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe(async (result) => {
      await this.loadDrivers();
      if (result) {
        this.snackBar.open('Driver updated sucessfully', 'Ok', {
          duration: 4000, // Keep it visible for 4 seconds
          verticalPosition: 'top', // Show at the top
          horizontalPosition: 'center' // Centered horizontally
        });
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/drivers']);
  }
}
