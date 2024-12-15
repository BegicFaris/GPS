import { Component, inject } from '@angular/core';
import { LineService } from '../../_services/line.service';
import { Line } from '../../_models/line';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { DriverEditComponent} from '../driver-edit/driver-edit.component';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { DriverService } from '../../_services/driver.service';
import { Driver } from '../../_models/driver';

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

  drivers: Driver[] = [];

  ngOnInit() {
    this.titleService.setTitle('Drivers');
    this.loadDrivers();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/drivers') {
        this.loadDrivers();
      }
    });
  }
  loadDrivers() {
    this.driverService.getAllDrivers().subscribe((data) => {
      this.drivers = data; // or data.lines if it's nested
      console.log(this.drivers);
    });
  }
  deleteDriver(id: number) {
    if (confirm('Are you sure you want to delete this driver?')) {
      this.driverService.deleteDriver(id).subscribe({
        next: (response) => {
          this.loadDrivers();
          console.log('Driver deleted successfully', response);
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting driver', error);
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
    dialogRef.afterClosed().subscribe((result) => {
      this.loadDrivers();
      if (result) {
        console.log('Updated Driver:', result);
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/drivers']);
  }
}
