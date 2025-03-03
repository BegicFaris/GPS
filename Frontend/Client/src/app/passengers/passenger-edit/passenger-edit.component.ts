import { Component, inject, Inject, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { LineService } from '../../_services/line.service';
import { NgIf } from '@angular/common';
import { StationService } from '../../_services/station.service';
import { Station } from '../../_models/station';
import { Title } from '@angular/platform-browser';
import { ManagerService } from '../../_services/manager.service';
import { ManagerLevel } from '../../_models/manager-level';
import { Department } from '../../_models/department';
import { DriverService } from '../../_services/driver.service';
import { PassengerService } from '../../_services/passenger.service';
import { DiscountService } from '../../_services/discount.service';
import { Discount } from '../../_models/discount';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-passenger-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './passenger-edit.component.html',
  styleUrl: './passenger-edit.component.css',
})
export class PassengerEditComponent {
  @ViewChild('updatePassengerForm') updatePassengerForm!: NgForm;
  formSubmitted = false;

  constructor(
    public dialogRef: MatDialogRef<PassengerEditComponent>,
    @Inject(MAT_DIALOG_DATA)
    public passengerUpdate: { id: number; firstName: string, lastName: string, email: string, birthDate: Date, address: string, registrationDate: Date, discountId: number }
  ) { }

  private titleService = inject(Title);
  private passengerService = inject(PassengerService);
  private discountService = inject(DiscountService);
  discounts: Discount[] = [];

  ngOnInit(): void {
    this.titleService.setTitle('Update passenger');
    this.loadDiscounts();
  }

  loadDiscounts() {
    this.discountService.getAllDiscounts().subscribe((data) => {
      this.discounts = data // or data.lines if it's nested
    });
  }
  async saveChanges() {
    try {
      await firstValueFrom(this.passengerService.updatePassenger(this.passengerUpdate));
      this.dialogRef.close(this.passengerUpdate);
    }
    catch (err) {
      console.error(err);
    }

  }
  closeDialog() {
    this.dialogRef.close();
  }
}
