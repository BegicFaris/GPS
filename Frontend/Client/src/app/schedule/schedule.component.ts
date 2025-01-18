import { Component, Inject, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NavigationEnd, Router, RouterLink, } from '@angular/router';
import { ScheduleService } from '../_services/shcedule.service';
import { FavoriteLineService } from '../_services/favorite-line.service';
import { LineService } from '../_services/line.service';
import { Line } from '../_models/line';
import { CommonModule } from '@angular/common';
import { FavoriteLine } from '../_models/favorite-line';
import { AccountService } from '../_services/account.service';
import { MyAppUserService } from '../_services/my-app-user.service';
import { User } from '../_models/user';
import { combineLatest, debounceTime, firstValueFrom, map, Observable, startWith, switchMap } from 'rxjs';
import { RouteService } from '../_services/route.service';
import { Schedule } from '../_models/schedule';
import { Route } from '../_models/route';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Station } from '../_models/station';
import { StationService } from '../_services/station.service';

@Component({
  selector: 'app-schedule',
  standalone: true,
  imports: [RouterLink, CommonModule, MatAutocompleteModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './schedule.component.html',
  styleUrl: './schedule.component.css'
})
export class ScheduleComponent {
  private titleService = inject(Title);
  private scheduleService = inject(ScheduleService);
  private routeService = inject(RouteService);
  private stationService = inject(StationService);
  private favoriteLineService = inject(FavoriteLineService);
  private lineService = inject(LineService);
  private accountService = inject(AccountService);
  private myAppUserService = inject(MyAppUserService);
  private router = inject(Router);


  lineAutocompleteFormControl = new FormControl('');
  filteredLines$: Observable<Line[]>;


  stationAutocompleteFormControl = new FormControl('');
  filteredStations$: Observable<Station[]>;


  selectedLine: Line | null = null;
  allLines: Line[] = [];
  searchLines: Line[] = [];
  allStations: Station[] = [];
  routes: Route[] = [];
  schedules: Schedule[] = [];
  selectedSchedule: Schedule = { id: 0, lineId: 0, departureTime: "0" };
  favoriteLineArray: Line[] = [];
  displayLines: Line[] = [];
  favoriteLines: FavoriteLine[] = [];
  public currentUser: User | null = this.accountService.currentUser();
  private currentUserId: number | null = null;

  constructor() {
    this.filteredLines$ = this.lineAutocompleteFormControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filterLines(value || ''))
    );

    this.filteredStations$ = this.stationAutocompleteFormControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filterStations(value || ''))
    );
  }
  private _filterLines(value: string): Line[] {
    const filterValue = this.normalizeString(value);
    return this.allLines.filter(line => this.normalizeString(line.name).includes(filterValue));
  }
  private _filterStations(value: string): Station[] {
    const filterValue = this.normalizeString(value);
    return this.allStations.filter(station => this.normalizeString(station.name).includes(filterValue));
  }

  private normalizeString(str: string): string {
    return str
      .toLowerCase() // Convert to lowercase
      .replace(/č|ć/g, 'c') // Normalize Č and Ć to C
      .replace(/š/g, 's') // Normalize Š to S
      .replace(/đ/g, 'd') // Normalize Đ to D
      .replace(/ž/g, 'z') // Normalize Ž to Z
      .replace(/dj/g, 'd') // Handle Đ as Dj
      .replace(/-/g, '') // Remove hyphens
      .replace(/\s+/g, ' ') // Replace multiple spaces with a single space
      .trim(); // Remove leading and trailing spaces
  }

  async ngOnInit() {
    this.titleService.setTitle("Schedule");
    await this.LoadLines();
    await this.LoadStations();
    if (this.currentUser != null) {
      await this.setCurrentUserId();
      this.LoadFavoriteLines();
    }
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd && event.url === '/schedule') {
        await this.LoadLines();
        await this.LoadStations();
        if (this.currentUser != null) {
          await this.setCurrentUserId();
          this.LoadFavoriteLines();
        }
      }
    });
    this.listenToSearchFields();
  }
  async LoadStations() {
    this.allStations = await firstValueFrom(this.stationService.getAllStations());
  }
  async setCurrentUserId(): Promise<boolean> {
    try {
      const user = await firstValueFrom(
        this.myAppUserService.getMyAppUserByEmail(this.accountService.getUserEmail())
      );
      this.currentUserId = user.id;
      console.log(this.currentUserId);
      return true;
    } catch (error) {
      console.error('Failed to set current user ID:', error);
      return false;
    }
  }
  async LoadLines() {
    this.allLines = await firstValueFrom(this.lineService.getAllLines());
    this.displayLines = this.allLines;
    this.searchLines = this.allLines;
  }
  async LoadFavoriteLines() {
    if (this.currentUserId != null) {
      try {
        console.log("Getting favorite lines: " + this.currentUserId);
        const data = await firstValueFrom(this.favoriteLineService.GetFavoriteLineByUserId(this.currentUserId));
        console.log("Updated favorite lines:", data);  // Log the data received
        this.favoriteLines = data;
      } catch (error) {
        console.error('Error loading favorite lines', error);
      }
    }
  }
  async selectLine(line: Line) {

    const sl = this.selectedLine?.id === line.id ? null : line
    if (sl) {
      await this.loadRoutes(sl.id);
      await this.loadShchedules(sl.id);
    }
    this.selectedLine = this.selectedLine?.id === line.id ? null : line;

    setTimeout(() => {
      const scheduleDropdown = document.getElementById('Schedule') as HTMLSelectElement;
      if (scheduleDropdown) {
        this.schedulesTimeChange({ target: scheduleDropdown });
      }
    }, 0);
  }

  async loadRoutes(lineId: number) {
    try {
      this.routes = await firstValueFrom(this.routeService.getAllRoutesByLineId(lineId));
    } catch (error) {
      console.error('Failed to load routes:', error);
    }
  }
  async loadShchedules(lineId: number) {
    try {
      this.schedules = await firstValueFrom(this.scheduleService.getAllSchedulesByLineId(lineId));
    } catch (error) {
      console.error('Failed to load routes:', error);
    }
  }
  async toggleFavorite(lineID: number, event: MouseEvent,) {
    event.stopPropagation();
    const iconElement = event.target as HTMLElement;
    if (this.isFavorite(lineID) == false) {
      if (this.currentUserId != null) {
        const favoriteLine = {
          userId: this.currentUserId,
          lineId: lineID
        };
        await firstValueFrom(this.favoriteLineService.CreateFavoriteLine(favoriteLine));
        await this.LoadFavoriteLines();
        iconElement.classList.remove('bi-star');
        iconElement.classList.add('bi-star-fill');
      }
    }
    else {
      const favoriteLineId = this.favoriteLines.find(x => x.lineId === lineID)?.id;
      if (favoriteLineId) {
        await firstValueFrom(this.favoriteLineService.DeleteFavoriteLine(favoriteLineId));
        await this.LoadFavoriteLines();
        iconElement.classList.remove('bi-star-fill');
        iconElement.classList.add('bi-star');
      }
    }
    console.log(this.favoriteLines);
  }
  isFavorite(lineId: number): boolean {
    return this.favoriteLines.some(favoriteLine => favoriteLine.lineId === lineId);
  }
  async schedulesTimeChange(event: any) {
    const selectedScheduleId = event.target.value;
    this.selectedSchedule = await firstValueFrom(this.scheduleService.getSchedule(selectedScheduleId));
  }
  onFavoritesToggleChange(event: any): void {
    const isChecked = event.target.checked; // Whether the toggle is checked (true) or unchecked (false)
    console.log('Favorites Only:', isChecked);
    console.log(this.favoriteLines);

    if (isChecked) {

      this.favoriteLineArray = [];

      this.favoriteLines.forEach(favLine => {
        this.favoriteLineArray.push(favLine.line);
      });

      this.displayLines = this.favoriteLineArray.filter(favLine =>
        this.searchLines.some(searchLine => searchLine.id === favLine.id)
      );
    }
    else {
      this.displayLines = this.searchLines;
    }
  }
  calculateDepartureTime(distanceFromNextStation: string, i: number): string {
    // Convert departure time to minutes
    let result = this.convertToMinutes(this.selectedSchedule.departureTime);
    if (!result)
      return "";
    for (let index = 0; index < i; index++) {
      let timeInterval = this.convertToMinutes(this.routes[index].distanceFromTheNextStation);
      result += timeInterval;
    }

    // Convert result back to time string
    return this.formatTime(result);
  }
  private convertToMinutes(time: string): number {
    // Convert time in hh:mm:ss format to minutes
    const [hours, minutes] = time.split(':').map(num => parseInt(num, 10));
    const totalMinutes = hours * 60 + minutes;  // Convert hours to minutes and add the minutes
    return totalMinutes;
  }
  private formatTime(minutes: number): string {
    // Convert total minutes back to hh:mm format
    const hours = Math.floor(minutes / 60); // Get hours
    const mins = minutes % 60; // Get the remainder minutes
    return `${String(hours).padStart(2, '0')}:${String(mins).padStart(2, '0')}`;
  }


  listenToSearchFields(): void {
    // For Line Search input
    this.lineAutocompleteFormControl.valueChanges.pipe(
      debounceTime(300), // Debounce to avoid too many calls
      switchMap((lineName) => {
        return this.searchLinesByNameAndStation(lineName, this.stationAutocompleteFormControl.value);
      })
    ).subscribe();

    // For Station Search input
    this.stationAutocompleteFormControl.valueChanges.pipe(
      debounceTime(300), // Debounce to avoid too many calls
      switchMap((stationName) => {
        return this.searchLinesByNameAndStation(this.lineAutocompleteFormControl.value, stationName);
      })
    ).subscribe();
  }
  async searchLinesByNameAndStation(lineName: string | null, stationName: string | null): Promise<Line[]> {
    this.searchLines = await firstValueFrom(this.lineService.getAllLines(lineName, stationName));
    this.displayLines = this.searchLines;
    return this.displayLines;
  }




  downloadSchedule() {
    this.lineService.getSchedulePDF().subscribe((pdfBlob: Blob) => {
      const url = window.URL.createObjectURL(pdfBlob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'Schedule.pdf'; // File name for download
      a.click();
      window.URL.revokeObjectURL(url); // Clean up URL object
    }, error => {
      console.error('Error generating PDF:', error);
    });
  }

}
