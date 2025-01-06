
import { AbstractControl, ReactiveFormsModule, ValidationErrors, ValidatorFn } from '@angular/forms';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LineService } from '../../_services/line.service';
import { Line } from '../../_models/line';
import { NgFor, NgIf, Time } from '@angular/common';
import { Title } from '@angular/platform-browser';
import { ShiftDetailService } from '../../_services/shift-detail.service';
import { ShiftDetail } from '../../_models/shift-detail';
import { Shift } from '../../_models/shift';
import { first, firstValueFrom, identity, lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-shift-details',
  standalone: true,
  imports: [ReactiveFormsModule, NgFor, NgIf],
  templateUrl: './shift-details.component.html',
  styleUrl: './shift-details.component.css'
})
export class ShiftDetailsComponent implements OnInit {
  private router = inject(Router);
  private lineService = inject(LineService);
  private titleService = inject(Title);
  private shiftDetailsService = inject(ShiftDetailService);

  existingShiftDetails: ShiftDetail[] = [];
  shift: any;
  shiftDetails: FormGroup[] = [];
  lines: Line[] = [];

  constructor(private fb: FormBuilder, private route: ActivatedRoute) { }

  async ngOnInit() {
    this.titleService.setTitle("Shift details");

    await this.loadLines();

    const params = await firstValueFrom(this.route.queryParams);
    this.shift = params;

    await this.loadShiftDetails(this.shift.id);
    console.log(this.existingShiftDetails);
    this.loadFormGroup();
  }
  async loadShiftDetails(shiftId: number): Promise<void> {
    this.existingShiftDetails = await lastValueFrom(this.shiftDetailsService.GetShiftDetailsByShiftId(shiftId));
  }

  loadFormGroup() {
    this.existingShiftDetails.forEach((esd) => {
      this.shiftDetails.push(new FormGroup({
        shiftId: new FormControl(this.shift.id),
        lineId: new FormControl(esd.lineId, [Validators.required]),
        shiftDetailStartingTime: new FormControl(esd.shiftDetailStartingTime, [Validators.required, this.startTimeValidator()]),
        shiftDetailEndingTime: new FormControl(esd.shiftDetailEndingTime, [Validators.required,this.endingTimeValidator(),this.timeComparrisonValidator()]),
        isEditMode: new FormControl(true),
      }));
    });
    this.shiftDetails.push(new FormGroup({
      shiftId: new FormControl(this.shift.id),
      lineId: new FormControl(null, [Validators.required]),
      shiftDetailStartingTime: new FormControl((this.existingShiftDetails.length == 0) ? this.shift.shiftStartingTime : this.existingShiftDetails[this.existingShiftDetails.length - 1].shiftDetailEndingTime, 
      [Validators.required, this.startTimeValidator()]),
      shiftDetailEndingTime: new FormControl('',[Validators.required,this.endingTimeValidator(),this.timeComparrisonValidator()]),
      isEditMode: new FormControl(false),
    }));
  }

  async loadLines(): Promise<void> {
    this.lines = await firstValueFrom(this.lineService.getAllLines());
  }
  addShiftDetail(index: number) {
    if (this.shiftDetails[index].valid) {
      this.shiftDetails[index].get('isEditMode')?.setValue(true)
      const shiftDetail = this.fb.group({
        shiftId: new FormControl(this.shift.id),
        lineId: new FormControl(null, [Validators.required]),
        shiftDetailStartingTime: [
          this.shiftDetails[index].get('shiftDetailEndingTime')?.value,
          [Validators.required, this.startTimeValidator()]  // Start time validator added
        ],
        shiftDetailEndingTime: ['', [Validators.required,this.endingTimeValidator(),this.timeComparrisonValidator()]],
        isEditMode: new FormControl(false),
      });

      this.shiftDetails.push(shiftDetail);
    } else {
      Object.keys(this.shiftDetails[index].controls).forEach(field => {
        const control = this.shiftDetails[index].get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }

  deleteShiftDetail(index: number) {
    if (this.shiftDetails.length > 1) {
      this.shiftDetails.splice(index, 1);
      console.log(this.shiftDetails);
    } else {

      this.shiftDetails[0].patchValue({
        lineId: null,
        shiftDetailStartingTime: this.shift.shiftStartingTime,
        shiftDetailEndingTime: null,
      });
    }
  }

  async finishAddingShiftDetails() {
    if (this.isValidForm()) {
      if (this.isValidShiftTime()) {
        if(this.existingShiftDetails.length!=0)
        await firstValueFrom(this.shiftDetailsService.DeleteShiftDetailsByShiftId(this.shift.id));
        const newShiftDetails = this.mapFormGroupsToShiftDetails();
        console.log(newShiftDetails);
        newShiftDetails.forEach(
          (sd) => {
            if (sd.id == null) {
              this.shiftDetailsService.CreateShiftDetail(sd).subscribe();
            }
          }
        );
        this.router.navigate(['/manager-dashboard/shifts']);
      }
      else {
        alert('The ending time or starting  does not match the expected time for this shift.');
      }
    }
  }

  isValidShiftTime(): boolean {
    const shiftDetailStartingTime = this.shiftDetails[0].get('shiftDetailStartingTime')?.value;
    const normalizedShiftDetailStartingTime = this.normalizeTime(shiftDetailStartingTime);
    const normalizedShiftStartingTime = this.normalizeTime(this.shift.shiftStartingTime);

    const shiftDetailEndingTime = this.shiftDetails[this.shiftDetails.length - 2].get('shiftDetailEndingTime')?.value;
    const normalizedShiftDetailEndingTime = this.normalizeTime(shiftDetailEndingTime);
    const normalizedShiftEndingTime = this.normalizeTime(this.shift.shiftEndingTime);
    console.log("s: "+ normalizedShiftStartingTime);
    console.log("sd: "+ normalizedShiftDetailStartingTime);
    console.log("e: "+ normalizedShiftEndingTime);
    console.log("ed: "+ normalizedShiftDetailEndingTime);
    return normalizedShiftDetailEndingTime === normalizedShiftEndingTime && normalizedShiftDetailStartingTime === normalizedShiftStartingTime;
  }
  normalizeTime(time: string): string {
    const [hours, minutes, seconds = "00"] = time.split(':');
    return `${hours}:${minutes}:${seconds}`;
  }
  isValidForm(): boolean {
    for (let i = 0; i < this.shiftDetails.length - 1; i++) {
      if (!this.shiftDetails[i].valid)
        return false;
    }
    return true;
  }

  mapFormGroupsToShiftDetails(): ShiftDetail[] {
    return this.shiftDetails.slice(0, this.shiftDetails.length - 1).map(formGroup => {
      const { isEditMode, ...shiftData } = formGroup.value;
      return shiftData as ShiftDetail;
    });
  }
  isEndingTimeTooEarly(index: number): boolean {
    const startTime = this.parseTime(this.shiftDetails[index].get('shiftDetailStartingTime')?.value);
    const endTime = this.parseTime(this.shiftDetails[index].get('shiftDetailEndingTime')?.value);
    return endTime < startTime;
  }
  isEndingTimeTooLate(index: number): boolean {
    const shiftEndTime = this.parseTime(this.shift.shiftEndingTime);
    const endTime = this.parseTime(this.shiftDetails[index].get('shiftDetailEndingTime')?.value);
    return endTime > shiftEndTime;
  }
  parseTime(time: string): Date {
    const [hours, minutes, seconds] = time.split(':').map(Number);
    const date = new Date();
    date.setHours(hours, minutes, seconds || 0, 0);
    return date;
  }

  cancel() {
    this.router.navigate(['/manager-dashboard/shifts']);
  }

  startTimeValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
    
      const shiftDetailStartTimeValue = control.value;
  
      if (!shiftDetailStartTimeValue) {
        return null;  
      }
  
      const shiftStartTime = this.parseTime(this.shift.shiftStartingTime);
      const startTime = this.parseTime(shiftDetailStartTimeValue);
  
      const tooEarly = startTime < shiftStartTime; 
  
      return tooEarly ? { validStartTime: true } : null; 
    };
  }
  endingTimeValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {

      const shiftDetailEndTimeValue = control.value;

      if (!shiftDetailEndTimeValue) {
        return null;
      }

      const shiftEndTime = this.parseTime(this.shift.shiftEndingTime);

      const endTime = this.parseTime(shiftDetailEndTimeValue);
      const tooLate = endTime > shiftEndTime;

      return tooLate ? { validEndTime: true } : null;
    }
  }

  timeComparrisonValidator(): ValidatorFn {
    return (form: AbstractControl): ValidationErrors | null => {

      const endTime = form.get("shiftDetailEndingTime")?.value;
      const startTime = form.get("shiftDetailStartingTime")?.value;


      if (endTime && startTime) {
        const startDate = new Date('1970-01-01T' + startTime + 'Z');
        const endDate = new Date('1970-01-01T' + endTime + 'Z');


        const isTimeValid = endDate.getTime() > startDate.getTime();

        return isTimeValid ? null : { timeComparison : true };
      }

      return null;
    }
  }

}

