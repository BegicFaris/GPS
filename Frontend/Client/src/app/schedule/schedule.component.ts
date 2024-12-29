import { Component, Inject, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLink, } from '@angular/router';
import { ScheduleService } from '../_services/shcedule.service';
import { FavoriteLineService } from '../_services/favorite-line.service';
import { LineService } from '../_services/line.service';
import { Line } from '../_models/line';
import { CommonModule } from '@angular/common';
import { FavoriteLine } from '../_models/favorite-line';
import { AccountService } from '../_services/account.service';
import { MyAppUserService } from '../_services/my-app-user.service';
import { User } from '../_models/user';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-schedule',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './schedule.component.html',
  styleUrl: './schedule.component.css'
})
export class ScheduleComponent {
  private titleService = inject(Title);
  private scheduleService = inject(ScheduleService);
  private favoriteLineService = inject(FavoriteLineService);
  private lineService = inject(LineService);
  private accountService = inject(AccountService);
  private myAppUserService = inject(MyAppUserService);
  selectedLine: Line | null = null;
  lines: Line[] = [];
  favoriteLines: FavoriteLine[] = [];
  public currentUser: User | null = this.accountService.currentUser();
  private currentUserId: number | null = null;
  async ngOnInit() {
    this.titleService.setTitle("Schedule");
    this.LoadLines();
    if (this.currentUser != null) {
      await this.setCurrentUserId();
      this.LoadFavoriteLines();

    }
  }

  async setCurrentUserId(): Promise<boolean> {
    try {
      const user = await firstValueFrom(
        this.myAppUserService.getMyAppUserByEmail(this.accountService.getUserEmail())
      );
      this.currentUserId = user.id;
      return true;
    } catch (error) {
      console.error('Failed to set current user ID:', error);
      return false;
    }
  }
  LoadLines() {
    this.lineService.getAllLines().subscribe(
      (data) => {
        this.lines = data;
      });
  }
  async LoadFavoriteLines() {
    if (this.currentUserId != null) {
      try {
        const data = await firstValueFrom(this.favoriteLineService.GetFavoriteLineByUserId(this.currentUserId));
        console.log("Updated favorite lines:", data);  // Log the data received
        this.favoriteLines = data;
      } catch (error) {
        console.error('Error loading favorite lines', error);
      }
    }
  }
  selectLine(line: Line): void {
    this.selectedLine = this.selectedLine?.id === line.id ? null : line;
  }
  async toggleFavorite(lineID: number, event: MouseEvent,){
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
}

