
import { AbstractControl, FormArray, ReactiveFormsModule, ValidationErrors, ValidatorFn } from '@angular/forms';
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
import { Route } from '../../_models/route';
import { RouteService } from '../../_services/route.service';
import { Station } from '../../_models/station';
import { StationService } from '../../_services/station.service';

@Component({
  selector: 'app-route-details',
  standalone: true,
  imports: [ReactiveFormsModule, NgFor, NgIf],
  templateUrl: './route-details.component.html',
  styleUrl: './route-details.component.css'
})
export class RouteDetailsComponent implements OnInit {
  private router = inject(Router);
  private lineService = inject(LineService);
  private titleService = inject(Title);
  private routeService = inject(RouteService);
  private stationService = inject(StationService);


  routeCount: number = 0;
  routeCountArr: number[] = [];
  existingRoutes: Route[] = [];
  line: any;
  routesForms: FormGroup[] = [];
  stations: Station[] = [];

  constructor(private fb: FormBuilder, private route: ActivatedRoute) { }

  async ngOnInit() {
    this.titleService.setTitle("Route details");

    await this.loadStations();

    const params = await firstValueFrom(this.route.queryParams);
    this.line = params;

    await this.loadRoutes(this.line.id);
    this.loadFormGroup();
  }
  async loadRoutes(lineId: number): Promise<void> {
    try {
      this.existingRoutes = await firstValueFrom(this.routeService.getAllRoutesByLineId(lineId));
    } catch (error) {
      console.error('Failed to load routes:', error);
    }
  }

  loadFormGroup() {
    this.existingRoutes.forEach((er) => {
      this.routeCount++;
      this.populateArray();
      this.routesForms.push(new FormGroup({
        lineId: new FormControl(this.line.id),
        stationId: new FormControl(er.stationId, [Validators.required]),
        distanceFromTheNextStation: new FormControl(er.distanceFromTheNextStation, [Validators.required]),
        order: new FormControl(er.order, [
          Validators.required]),
        isEditMode: new FormControl(true),
      }));
    });

    this.routesForms.push(new FormGroup({
      lineId: new FormControl(this.line.id),
      stationId: new FormControl(null, [Validators.required]),
      distanceFromTheNextStation: new FormControl("", [Validators.required]),
      order: new FormControl("", [
        Validators.required,
      ]),
      isEditMode: new FormControl(false),
    }));
    this.routeCount++;
    this.populateArray();
  }
  async loadStations(): Promise<void> {
    this.stations = await firstValueFrom(this.stationService.getAllStations());
  } 
  addRoute(index: number) {
    if (this.routesForms[index].valid) {


      this.routesForms[index].get('isEditMode')?.setValue(true)
      const routesDetails = this.fb.group(
        {
          lineId: new FormControl(this.line.id),
          stationId: new FormControl(null, [Validators.required]),
          distanceFromTheNextStation: new FormControl("", [Validators.required]),
          order: new FormControl("", [Validators.required]),
          isEditMode: new FormControl(false),
        }// Add your validator here
      );

      this.routeCount++;
      this.populateArray();

      this.routesForms.push(routesDetails);
    } else {
      Object.keys(this.routesForms[index].controls).forEach(field => {
        const control = this.routesForms[index].get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }
  }

  deleteRoute(index: number) {
    if (this.routesForms.length > 1) {
      this.routesForms.splice(index, 1);
      this.routeCount++;
      this.populateArray();
    } else {

      this.routesForms[0].patchValue({
        stationId: null,
        distanceFromTheNextStation: "",
        order: "",
      });
    }
  }

  async finishAddingRoutes() {
    if (this.isValidForm()) {
      if (this.isValidOrder()) {

        if (this.existingRoutes.length != undefined || this.existingRoutes.length==0)
          await firstValueFrom(this.routeService.deleteAllRoutesByLineIdAsync(this.line.id));
        const newRoutes = this.mapFormGroupsToRoutes();
        await firstValueFrom(this.routeService.createRoute(newRoutes));
        this.router.navigate(['/manager-dashboard/routes']);
      }
      else{
        alert("Routes must have unique order, and order can not be larger than the number of routes.");
      }
    }
  }
  isValidOrder(): boolean {
    const routes = this.mapFormGroupsToRoutes();
    for (let i = 0; i < routes.length; i++) {
      for (let j = 0; j < routes.length; j++) {
        if(i!=j)
          if(routes[i].order==routes[j].order || routes[i].order>=this.routeCountArr.length){
            return false;
          }
      }
    }
    return true;
  }
  normalizeTime(time: string): string {
    const [hours, minutes, seconds = "00"] = time.split(':');
    return `${hours}:${minutes}:${seconds}`;
  }
  isValidForm(): boolean {
    for (let i = 0; i < this.routesForms.length - 1; i++) {
      if (!this.routesForms[i].valid)
        return false;
    }
    return true;
  }

  mapFormGroupsToRoutes(): Route[] {
    return this.routesForms.slice(0, this.routesForms.length - 1).map(formGroup => {
      const { isEditMode, ...routeData } = formGroup.value;

      return {
        ...routeData,
        lineId: Number(routeData.lineId),
        stationId: Number(routeData.stationId),
        distanceFromNextStation: routeData.distanceFromNextStation,
        order: Number(routeData.order),
      } as Route;
    });
  }

  parseTime(time: string): Date {
    const [hours, minutes, seconds] = time.split(':').map(Number);
    const date = new Date();
    date.setHours(hours, minutes, seconds || 0, 0);
    return date;
  }

  cancel() {
    this.router.navigate(['/manager-dashboard/routes']);
  }

  populateArray() {
    this.routeCountArr = [];
    for (let i = 1; i <= this.routeCount; i++) {
      this.routeCountArr.push(i);
    }
  }
}

