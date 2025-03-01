import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { BusService } from '../../_services/bus.service';
import { ShiftService } from '../../_services/shift.service';
import { DriverService } from '../../_services/driver.service';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { Bus } from '../../_models/bus';
import { Driver } from '../../_models/driver';
import { Shift } from '../../_models/shift';
import { DateValidatorDirective } from '../../validators/date.validator';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-shift-create',
  standalone: true,
  imports: [FormsModule, DateValidatorDirective],
  templateUrl: './shift-create.component.html',
  styleUrl: './shift-create.component.css'
})
export class ShiftCreateComponent {

  today: string = new Date().toISOString().split('T')[0];  // Today's date
  futureDate: string = new Date(new Date().setFullYear(new Date().getFullYear() + 1)).toISOString().split('T')[0]; 

  private router = inject(Router);
  private shiftService = inject(ShiftService);
  private busService = inject(BusService);
  private driverService = inject(DriverService);
  private titleService = inject(Title);

  buses: Bus[] = [];
  drivers: Driver[] = [];
  shiftCreate: any = {};

  ngOnInit() {
    this.titleService.setTitle("Add shift");
    this.LoadBuses();
    this.LoadDrivers();
  }

  async addNewShift(newShiftForm: NgForm) {
    if (newShiftForm.valid && !this.isEndingTimeInvalid()) {
      try{
        await firstValueFrom(this.shiftService.createShift(this.shiftCreate));
        this.cancel();
      }
      catch(err){
          console.error(err);
      }
    }
    else {
      Object.keys(newShiftForm.controls).forEach(field => {
        const control = newShiftForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      }); 
    }
  }

  cancel() {
    this.router.navigate(['/manager-dashboard/shifts']);
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
    if(this.shiftCreate.shiftStartingTime==null || this.shiftCreate.shiftEndingTime==null)
      return false;
    const startTime = this.parseTime(this.shiftCreate.shiftStartingTime);
    const endTime = this.parseTime(this.shiftCreate.shiftEndingTime);
    return endTime < startTime;
  }
  parseTime(time: string): Date {
    const [hours, minutes, seconds] = time.split(':').map(Number);
    const date = new Date();
    date.setHours(hours, minutes, seconds || 0, 0); // Set time on current date
    return date;
  }
}
