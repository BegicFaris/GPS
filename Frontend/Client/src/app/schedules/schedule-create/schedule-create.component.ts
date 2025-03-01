import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { ManagerService } from '../../_services/manager.service';
import { StationService } from '../../_services/station.service';
import { Station } from '../../_models/station';
import { ZoneService } from '../../_services/zone.service';
import { Zone } from '../../_models/zone';
import { ScheduleService } from '../../_services/shcedule.service';
import { LineService } from '../../_services/line.service';
import { Schedule } from '../../_models/schedule';
import { Line } from '../../_models/line';
import { firstValueFrom } from 'rxjs';


@Component({
  selector: 'app-schedule-create',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './schedule-create.component.html',
  styleUrl: './schedule-create.component.css',
})
export class ScheduleCreateComponent {
  private scheduleService = inject(ScheduleService);
  private router = inject(Router);
  private lineService= inject(LineService);
  private titleService = inject(Title);
  schedules: Schedule[] = [];
  lines: Line[] = [];
  errorMessage: string = '';
  registerForm: FormGroup;



  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      lineId: [null, Validators.required],
      departureTime: ['', Validators.required],
    });
  }
  ngOnInit() {
    this.titleService.setTitle('Add schedule');
    this.loadExistingSchedules();
    this.loadExistingLines();
  }
  loadExistingLines() {
    this.lineService.getAllLines().subscribe({
      next: (lines) => {
        this.lines = lines;
      },
      error: (error) => {
        console.error('Error loading lines:', error);
      }
    });
  }

  loadExistingSchedules() {
    this.scheduleService.getAllSchedules().subscribe({
      next: (schedules) => {
        this.schedules = schedules;
      },
      error: (error) => {
        console.error('Error loading schedules:', error);
      }
    });
  }

  scheduleCreate: any = {};
 async  addNewSchedule(newScheduleForm: NgForm) {

    if (newScheduleForm.valid) {
      if (this.scheduleCreate.isActive === undefined) {
        this.scheduleCreate.isActive = false;
      }
      try{
        await firstValueFrom(this.scheduleService.createSchedule(this.scheduleCreate));
        this.cancel();
      }
      catch(err){
          console.error(err);
      }
    }
    else{
      Object.keys(newScheduleForm.controls).forEach(field => {
        const control = newScheduleForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }
  cancel() {
    this.router.navigate(['/manager-dashboard/schedules']);
  }


}
