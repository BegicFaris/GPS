import { Component, inject, Inject, NgZone, ViewChild } from '@angular/core';
import { FormsModule, NgForm, } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { LineService } from '../../_services/line.service';
import { CommonModule, NgIf } from '@angular/common';
import { StationService } from '../../_services/station.service';
import { Station } from '../../_models/station';
import { Title } from '@angular/platform-browser';
import { ManagerService } from '../../_services/manager.service';
import { ManagerLevel } from '../../_models/manager-level';
import { Department } from '../../_models/department';
import { LettersOnlyValidatorDirective } from '../../validators/only-letters.validator';
import { DateValidatorDirective } from '../../validators/date.validator';
import { firstValueFrom } from 'rxjs';
import { MyAppUserService } from '../../_services/my-app-user.service';

@Component({
  selector: 'app-manager-edit',
  standalone: true,
  imports: [FormsModule,LettersOnlyValidatorDirective, DateValidatorDirective, CommonModule],
  templateUrl: './manager-edit.component.html',
  styleUrl: './manager-edit.component.css'
})
export class ManagerEditComponent {
  @ViewChild('updateManagerForm') updateManagerForm!: NgForm;
  formSubmitted = false;
  emailExists: boolean = false;
  originalEmail: any;

  
  constructor(public dialogRef: MatDialogRef<ManagerEditComponent>,private ngZone: NgZone, @Inject(MAT_DIALOG_DATA) public managerUpdate: { id: number, firstName: string, lastName: string, email:string, birthDate: Date, address: string, hireDate:Date, department: string, managerLevel:string }) {}

  
  private titleService = inject(Title);
  private managerService = inject(ManagerService);
   private myAppUserService = inject(MyAppUserService);
  stations: Station[] = [];
  maxDate: string = new Date().toISOString().split('T')[0];

  managerLevels: ManagerLevel[] = [
    { id: 1, name: 'Junior Manager' },
    { id: 2, name: 'Mid-Level Manager' },
    { id: 3, name: 'Senior Manager' }
  ];

  departments: Department[] = [
    { id: 1, name: 'Management' },
    { id: 2, name: 'Human Resources' },
    { id: 3, name: 'Finance' },
    { id: 4, name: 'IT' },
    { id: 5, name: 'Marketing' }
  ];

  ngOnInit(): void {
    this.titleService.setTitle("Update manager");
    this.originalEmail = this.managerUpdate.email; // Store the original email
  }

  async saveChanges() {
    this.formSubmitted = true;
    if (this.updateManagerForm.form.valid) {
      if (this.managerUpdate.email !== this.originalEmail) {
        const emailCheck = await firstValueFrom(this.myAppUserService.checkEmailExists(this.managerUpdate.email));
        if (emailCheck.exists) {
          this.emailExists = true;
          return; // Stop form submission if email already exists
        }
      }

      this.emailExists = false;
      try{
      await firstValueFrom(this.managerService.updateManager(this.managerUpdate));
      this.dialogRef.close(this.managerUpdate);
      }
      catch(err){
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

