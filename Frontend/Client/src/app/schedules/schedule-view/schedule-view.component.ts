import { Component, inject } from '@angular/core';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { ScheduleService } from '../../_services/shcedule.service';
import { Schedule } from '../../_models/schedule';
import { ScheduleEditComponent } from '../schedule-edit/schedule-edit.component';

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
  schedules: Schedule[] = [];

  ngOnInit() {
    this.titleService.setTitle('Schedule');
    this.loadSchedules();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/schedules') {
        this.loadSchedules();
      }
    });
  }
  loadSchedules() {
    this.scheduleService.getAllSchedules().subscribe((data) => {
      this.schedules = data; // or data.lines if it's nested
      console.log(this.schedules);
    });
  }
  deleteSchedule(id: number) {
    if (confirm('Are you sure you want to delete this schedule?')) {
      this.scheduleService.deleteSchedule(id).subscribe({
        next: (response) => {
          this.loadSchedules();
          console.log('Schedule deleted successfully', response);
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
    dialogRef.afterClosed().subscribe((result) => {
      this.loadSchedules();
      if (result) {
        console.log('Updated Schedule:', result);
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/schedules']);
  }
}
