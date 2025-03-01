import { Component, inject } from '@angular/core';
import { LineService } from '../../_services/line.service';
import { Line } from '../../_models/line';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { PassengerEditComponent } from '../passenger-edit/passenger-edit.component';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { DriverService } from '../../_services/driver.service';
import { Driver } from '../../_models/driver';
import { PassengerService } from '../../_services/passenger.service';
import { Passenger } from '../../_models/passenger';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-passenger-view',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule, CommonModule],
  templateUrl: './passenger-view.component.html',
  styleUrl: './passenger-view.component.css',
})
export class PassengerViewComponent {
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  private passengerService = inject(PassengerService);

  passengers: Passenger[] = [];

  async ngOnInit() {
    this.titleService.setTitle('Passengers');
    await this.loadPassengers();
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd && event.url === '/passengers') {
        await this.loadPassengers();
      }
    });
  }
  async loadPassengers() {

    try {
      this.passengers = await firstValueFrom(this.passengerService.getAllPassengers());
    }
    catch (err) {
      console.error(err);
    }
  }
  deletePassenger(id: number) {
    if (confirm('Are you sure you want to delete this passenger?')) {
      this.passengerService.deletePassenger(id).subscribe({
        next: async (response) => {
          await this.loadPassengers();
          console.log('Passenger deleted successfully', response);
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting passenger', error);
        },
      });
    }
  }
  openEditDialog(passenger: Passenger) {
    const dialogRef = this.dialog.open(PassengerEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: passenger.id,
        firstName: passenger.firstName,
        lastName: passenger.lastName,
        email: passenger.email,
        birthDate: passenger.birthDate,
        address: passenger.address,
        registrationDate: passenger.registrationDate,
        discountId: passenger.discountID,
      }, // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe(async (result) => {
      await this.loadPassengers();
      if (result) {
        console.log('Updated Passenger:', result);
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/passengers']);
  }
}
