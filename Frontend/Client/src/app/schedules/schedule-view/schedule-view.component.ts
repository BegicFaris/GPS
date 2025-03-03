import { Component, inject } from '@angular/core';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { ScheduleService } from '../../_services/shcedule.service';
import { Schedule } from '../../_models/schedule';
import { ScheduleEditComponent } from '../schedule-edit/schedule-edit.component';
import { firstValueFrom } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-schedule-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule, CommonModule],
  templateUrl: './schedule-view.component.html',
  styleUrl: './schedule-view.component.css',
})
export class ScheduleViewComponent {
  private scheduleService = inject(ScheduleService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  private snackBar= inject(MatSnackBar);
  schedules: Schedule[] = [];

 async ngOnInit() {
    this.titleService.setTitle('Schedule');
   await this.loadSchedules();
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd && event.url === '/schedules') {
       await  this.loadSchedules();
      }
    });
  }
  async loadSchedules() {
    try{
        this.schedules=await firstValueFrom(this.scheduleService.getAllSchedules());
    }
    catch(err){
        console.error(err);
    }
  }
  deleteSchedule(id: number) {
    if (confirm('Are you sure you want to delete this schedule?')) {
      this.scheduleService.deleteSchedule(id).subscribe({
        next: async (response) => {
          await this.loadSchedules();
          this.snackBar.open('Schedule deleted sucessfully', 'Ok', {
            duration: 4000, // Keep it visible for 4 seconds
            verticalPosition: 'top', // Show at the top
            horizontalPosition: 'center' // Centered horizontally
          });
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting schedule', error);
        },
      });
    }
  }
  openEditDialog(schedule: Schedule) {
    const dialogRef = this.dialog.open(ScheduleEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: schedule.id,
        lineId: schedule.lineId,
        departureTime: schedule.departureTime,
      }, // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe(async (result) => {
      await this.loadSchedules();
      if (result) {
        this.snackBar.open('Schedule updated sucessfully', 'Ok', {
          duration: 4000, // Keep it visible for 4 seconds
          verticalPosition: 'top', // Show at the top
          horizontalPosition: 'center' // Centered horizontally
        });
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/schedules']);
  }
}
