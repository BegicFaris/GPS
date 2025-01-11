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
import { ZoneService } from '../../_services/zone.service';
import { Zone } from '../../_models/zone';
import { ScheduleService } from '../../_services/shcedule.service';
import { Schedule } from '../../_models/schedule';
import { Line } from '../../_models/line';

@Component({
  selector: 'app-schedule-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './schedule-edit.component.html',
  styleUrl: './schedule-edit.component.css'
})
export class ScheduleEditComponent {
  @ViewChild('updateScheduleForm') updateScheduleForm!: NgForm;
  formSubmitted = false;
  
  constructor(public dialogRef: MatDialogRef<ScheduleEditComponent>, 
    @Inject(MAT_DIALOG_DATA) public scheduleUpdate: 
    { 
      id: number, 
      lineId: number, 
      departureTime: string
    }) {}

  
  private titleService = inject(Title);
  private scheduleService = inject(ScheduleService);
  private lineService= inject(LineService);
  schedule: Schedule[] = [];
  lines:Line[]=[];

  ngOnInit(): void {
    this.titleService.setTitle("Update schedule");
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

  saveChanges() {
    this.formSubmitted = true;
    if (this.updateScheduleForm.form.valid) {
      this.scheduleService.updateSchedule(this.scheduleUpdate).subscribe({
        next: response => {
          console.log('Schedule updated successfully', response);
          this.dialogRef.close(this.scheduleUpdate);
        },
        error: error => {
          console.error('Error updating schedule', error);
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

