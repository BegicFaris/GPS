import { Component, inject, Inject, ViewChild } from '@angular/core';
import { FormsModule, NgForm, } from '@angular/forms';
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
import { LettersNumbersValidatorDirective } from '../../validators/letters-numbers.validator';
import { LettersOnlyValidatorDirective } from '../../validators/only-letters.validator';
import { LettersNumbersDashesValidatorDirective } from '../../validators/letters-numbers-dashes.validator';
import { DateValidatorDirective } from '../../validators/date.validator';

@Component({
  selector: 'app-driver-edit',
  standalone: true,
  imports: [FormsModule,LettersNumbersValidatorDirective,LettersOnlyValidatorDirective,LettersNumbersDashesValidatorDirective, DateValidatorDirective],
  templateUrl: './driver-edit.component.html',
  styleUrl: './driver-edit.component.css'
})
export class DriverEditComponent {
  @ViewChild('updateDriverForm') updateDriverForm!: NgForm;
  emailExists: boolean = false;
  
  constructor(public dialogRef: MatDialogRef<DriverEditComponent>, @Inject(MAT_DIALOG_DATA) public driverUpdate: { id: number, firstName: string, lastName: string, email:string, birthDate: Date, address: string, hireDate:Date, license: string, driversLicenseNumber: string, workingHoursInAWeek: number  }) {}

  
  private titleService = inject(Title);
  private driverService = inject(DriverService);
  maxDate: string = new Date().toISOString().split('T')[0];

  ngOnInit(): void {
    this.titleService.setTitle("Update driver");
  }

  saveChanges() {
    if (this.updateDriverForm.form.valid) {
      this.driverService.updateDriver(this.driverUpdate).subscribe({
        next: response => {
          console.log('Driver updated successfully', response);
          this.dialogRef.close(this.driverUpdate);
        },
        error: error => {
          console.error('Error updating driver', error);
        }
      });
    } else {
      console.log('Form is invalid. Please check the errors.');
    }
  }
  closeDialog() {
    this.dialogRef.close();
  }
}

