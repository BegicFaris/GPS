import { Component, inject } from '@angular/core';
import { ShiftService } from '../../_services/shift.service';
import { MatDialog } from '@angular/material/dialog';
import { Title } from '@angular/platform-browser';
import { NavigationEnd, Router } from '@angular/router';
import { Shift } from '../../_models/shift';
import { ShiftEditComponent } from '../shift-edit/shift-edit.component';
import { FormsModule } from '@angular/forms';
import { ShiftDetailService } from '../../_services/shift-detail.service';
import { firstValueFrom } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-shift-view',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './shift-view.component.html',
  styleUrl: './shift-view.component.css'
})
export class ShiftViewComponent {
  private shiftService = inject(ShiftService);
  private shiftDetailService = inject(ShiftDetailService);
  private router = inject(Router);
  private titleService = inject(Title);
  private dialog = inject(MatDialog);
  private snackBar= inject(MatSnackBar);
  shifts: Shift[] = [];


  async ngOnInit() {
    this.titleService.setTitle("Shifts");
    await this.loadShifts();
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd && event.url === '/manager-dashboard/notifications') {
        await this.loadShifts();
      }
    });
  }
  async loadShifts() {
    try {
      this.shifts = await firstValueFrom(this.shiftService.getAllShifts());
    }
    catch (err) {
      console.error(err);
    }

  }
  deleteShift(id: number) {
    if (confirm('Are you sure you want to delete this shift?')) {
      this.shiftService.deleteShift(id).subscribe({
        next: async (response) => {
          await this.loadShifts();
          this.snackBar.open('Shift deleted sucessfully', 'Ok', {
            duration: 4000, // Keep it visible for 4 seconds
            verticalPosition: 'top', // Show at the top
            horizontalPosition: 'center' // Centered horizontally
          });
          this.cancel();
        },
        error: error => {
          console.error('Error deleting shift', error);
        }
      });
    }
  }

  openEditDialog(shift: Shift) {
    const dialogRef = this.dialog.open(ShiftEditComponent, {
      height: '800px',
      width: '1000px',
      data: {
        id: shift.id,
        busId: shift.busId,
        driverId: shift.driverId,
        shiftDate: shift.shiftDate,
        shiftStartingTime: shift.shiftStartingTime,
        shiftEndingTime: shift.shiftEndingTime
      },
    });
    dialogRef.afterClosed().subscribe(async () => {
      await this.loadShifts();
      this.snackBar.open('Shift updated sucessfully', 'Ok', {
        duration: 4000, // Keep it visible for 4 seconds
        verticalPosition: 'top', // Show at the top
        horizontalPosition: 'center' // Centered horizontally
      });
      
    });
  }
  cancel() {
    this.router.navigate(['/manager-dashboard/shifts']);
  }
  goToAddForm() {
    this.router.navigate(['/manager-dashboard/shifts/add']);
  }
  openShiftDetails(shift: Shift) {
    this.router.navigate(['/manager-dashboard/shifts/details'], { queryParams: shift });
  }

  downloadShiftDetails(shiftId: number) {
    this.shiftDetailService.GetShiftDetailPdf(shiftId).subscribe((pdfBlob: Blob) => {
      const url = window.URL.createObjectURL(pdfBlob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'ShiftOverviewReport.pdf'; // File name for download
      a.click();
      window.URL.revokeObjectURL(url); // Clean up URL object
    }, error => {
      console.error('Error generating PDF:', error);
    });
  }

}
