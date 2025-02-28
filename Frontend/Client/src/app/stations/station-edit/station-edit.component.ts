import { Component, inject, Inject, ViewChild } from '@angular/core';
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
import { ZoneService } from '../../_services/zone.service';
import { Zone } from '../../_models/zone';
import { GpsCodeValidatorDirective } from '../../validators/gps-code.validator';
import { LettersNumbersValidatorDirective } from '../../validators/letters-numbers.validator';

@Component({
  selector: 'app-station-edit',
  standalone: true,
  imports: [FormsModule,LettersNumbersValidatorDirective,GpsCodeValidatorDirective,CommonModule],
  templateUrl: './station-edit.component.html',
  styleUrl: './station-edit.component.css'
})
export class StationEditComponent {
  @ViewChild('updateStationForm') updateStationForm!: NgForm;
  formSubmitted = false;
  
  constructor(public dialogRef: MatDialogRef<StationEditComponent>, @Inject(MAT_DIALOG_DATA) public stationUpdate: { id: number, name: string, location: string, gpsCode:string, zoneId: number}) {}

  
  private titleService = inject(Title);
  private stationService = inject(StationService);
  private zoneService= inject(ZoneService);
  zones:Zone[]=[];

  ngOnInit(): void {
    this.titleService.setTitle("Update station");
    console.log(this.stationUpdate)
    this.loadExistingZones();
  }
  loadExistingZones() {
    this.zoneService.getAllZones().subscribe({
      next: (zones) => {
        this.zones = zones;
      },
      error: (error) => {
        console.error('Error loading zones:', error);
      }
    });
  }

  saveChanges() {
    this.formSubmitted = true;
    if (this.updateStationForm.form.valid) {
      this.stationService.updateStation(this.stationUpdate).subscribe({
        next: response => {
          console.log('Station updated successfully', response);
          this.dialogRef.close(this.stationUpdate);
        },
        error: error => {
          console.error('Error updating station', error);
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

