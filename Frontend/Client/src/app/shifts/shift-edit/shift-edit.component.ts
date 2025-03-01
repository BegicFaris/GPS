import { Component, inject, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Title } from '@angular/platform-browser';
import { BusService } from '../../_services/bus.service';
import { DriverService } from '../../_services/driver.service';
import { ShiftService } from '../../_services/shift.service';
import { Bus } from '../../_models/bus';
import { Driver } from '../../_models/driver';
import { FormsModule, NgForm } from '@angular/forms';
import { DateValidatorDirective } from '../../validators/date.validator';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-shift-edit',
  standalone: true,
  imports: [FormsModule, DateValidatorDirective],
  templateUrl: './shift-edit.component.html',
  styleUrl: './shift-edit.component.css'
})
export class ShiftEditComponent {
  today: string = new Date().toISOString().split('T')[0];  // Today's date
  futureDate: string = new Date(new Date().setFullYear(new Date().getFullYear() + 1)).toISOString().split('T')[0];

  constructor(public dialogRef: MatDialogRef<ShiftEditComponent>, @Inject(MAT_DIALOG_DATA) public shiftUpdate:
    {
      id: number,
      busId: number,
      driverId: number,
      shiftDate: string,
      shiftStartingTime: string,
      shiftEndingTime: string
    }) { }

  private shiftService = inject(ShiftService);
  private busService = inject(BusService);
  private driverService = inject(DriverService);
  private titleService = inject(Title);


  buses: Bus[] = [];
  drivers: Driver[] = [];

  ngOnInit() {
    this.titleService.setTitle("Edit shift");
    this.LoadBuses();
    this.LoadDrivers();
  }

  async saveChanges(updateShiftForm: NgForm) {
    if (updateShiftForm.valid && !this.isEndingTimeInvalid()) {
      try {
        await firstValueFrom(this.shiftService.updateShift(this.shiftUpdate));
        this.dialogRef.close(this.shiftUpdate);
      }
      catch (err) {
        console.error(err);
      }
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }

  LoadDrivers() {
    this.driverService.getAllDrivers().subscribe(
      (data) => {
        this.drivers = data;
      });
  }
  LoadBuses() {
    this.busService.getAllBuses().subscribe(
      (data) => {
        this.buses = data;
      });
  }

  isEndingTimeInvalid(): boolean {
    const startTime = this.parseTime(this.shiftUpdate.shiftStartingTime);
    const endTime = this.parseTime(this.shiftUpdate.shiftEndingTime);
    return endTime < startTime;
  }

  parseTime(time: string): Date {
    const [hours, minutes, seconds] = time.split(':').map(Number);
    const date = new Date();
    date.setHours(hours, minutes, seconds || 0, 0); // Set time on current date
    return date;
  }
}
