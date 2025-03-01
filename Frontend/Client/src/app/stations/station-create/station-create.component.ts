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
import { LettersNumbersValidatorDirective } from '../../validators/letters-numbers.validator';
import { GpsCodeValidatorDirective } from '../../validators/gps-code.validator';
import { firstValueFrom } from 'rxjs';


@Component({
  selector: 'app-station-create',
  standalone: true,
  imports: [FormsModule, LettersNumbersValidatorDirective, GpsCodeValidatorDirective, CommonModule, RouterLink],
  templateUrl: './station-create.component.html',
  styleUrl: './station-create.component.css',
})
export class StationCreateComponent {
  private stationService = inject(StationService);
  private router = inject(Router);
  private zoneService = inject(ZoneService);
  private titleService = inject(Title);
  stations: Station[] = [];
  zones: Zone[] = [];
  errorMessage: string = '';
  stationExists: boolean = false;
  registerForm: FormGroup;



  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      zoneId: [null, Validators.required],
      name: ['', Validators.required],
      location: ['', [Validators.required, Validators.email]],
      gpsCode: ['', Validators.required],
    });
  }
  async ngOnInit() {
    this.titleService.setTitle('Add station');
    this.loadExistingStations();
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

  loadExistingStations() {
    this.stationService.getAllStations().subscribe({
      next: (stations) => {
        this.stations = stations;
      },
      error: (error) => {
        console.error('Error loading stations:', error);
      }
    });
  }

  stationCreate: any = {};
  async addNewStation(newStationForm: NgForm) {

    if (newStationForm.valid && !this.stationExists) {
      if (this.stationCreate.isActive === undefined) {
        this.stationCreate.isActive = false;
      }
      try {
        await firstValueFrom(this.stationService.createStation(this.stationCreate));
        this.cancel();
      }
      catch (err) {
        console.error(err);
      }
    }
    else {
      Object.keys(newStationForm.controls).forEach(field => {
        const control = newStationForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }
  cancel() {
    this.router.navigate(['/manager-dashboard/stations']);
  }



  checkStationExists() {
    if (this.stationCreate.name) {
      this.stationExists = this.stations.some(station => station.name === this.stationCreate.name);
    }
  }
}
