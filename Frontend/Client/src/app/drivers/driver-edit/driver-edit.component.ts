import { Component, inject, Inject, NgZone, ViewChild } from '@angular/core';
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
import { firstValueFrom } from 'rxjs';
import { MyAppUserService } from '../../_services/my-app-user.service';

@Component({
  selector: 'app-driver-edit',
  standalone: true,
  imports: [FormsModule, LettersNumbersValidatorDirective, LettersOnlyValidatorDirective, LettersNumbersDashesValidatorDirective, DateValidatorDirective],
  templateUrl: './driver-edit.component.html',
  styleUrl: './driver-edit.component.css'
})
export class DriverEditComponent {
  @ViewChild('updateDriverForm') updateDriverForm!: NgForm;
  emailExists: boolean = false;
  originalEmail: any;

  constructor(
    public dialogRef: MatDialogRef<DriverEditComponent>, 
    private ngZone: NgZone,
    @Inject(MAT_DIALOG_DATA) public driverUpdate: 
    { id: number, 
      firstName: string, 
      lastName: string, 
      email: string, 
      birthDate: Date, 
      address: string, 
      hireDate: Date, 
      license: string, 
      driversLicenseNumber: string, 
      workingHoursInAWeek: number }
    
    ) { }


  private titleService = inject(Title);
  private driverService = inject(DriverService);
  private myAppUserService = inject(MyAppUserService);
  maxDate: string = new Date().toISOString().split('T')[0];


  ngOnInit(): void {
    this.titleService.setTitle("Update driver");
    this.originalEmail = this.driverUpdate.email; 
  }

  async saveChanges() {
    if (this.updateDriverForm.form.valid) {
      if (this.driverUpdate.email !== this.originalEmail) {
        const emailCheck = await firstValueFrom(this.myAppUserService.checkEmailExists(this.driverUpdate.email));
        if (emailCheck.exists) {
          this.emailExists = true;
          return; 
        }
      }
  
      this.emailExists = false;
      try {
        await firstValueFrom(this.driverService.updateDriver(this.driverUpdate));
        this.closeDialog();
      }
      catch (err) {
        console.error(err);
      }
    } else {
      console.log('Form is invalid. Please check the errors.');
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }
}

