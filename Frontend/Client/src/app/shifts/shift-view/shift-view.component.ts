import { Component, inject } from '@angular/core';
import { ShiftService } from '../../_services/shift.service';
import { MatDialog } from '@angular/material/dialog';
import { Title } from '@angular/platform-browser';
import { NavigationEnd, Router } from '@angular/router';
import { Shift } from '../../_models/shift';
import { ShiftEditComponent } from '../shift-edit/shift-edit.component';
import { FormsModule } from '@angular/forms';
import { ShiftDetailService } from '../../_services/shift-detail.service';

@Component({
  selector: 'app-shift-view',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './shift-view.component.html',
  styleUrl: './shift-view.component.css'
})
export class ShiftViewComponent {
  private shiftService = inject(ShiftService);
  private shiftDetailService=inject(ShiftDetailService);
  private router = inject(Router);
  private titleService = inject(Title);
  private dialog = inject(MatDialog)
  shifts: Shift[] = [];


 ngOnInit() {

    this.titleService.setTitle("Shifts");
    this.loadShifts();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/manager-dashboard/notifications') {
        this.loadShifts();
      }
    });
  }
  loadShifts() {
    this.shiftService.getAllShifts().subscribe(
      (data) => {
        this.shifts = data; 
      },
    );
  }
  deleteShift(id: number) {
    if (confirm('Are you sure you want to delete this shift?')) {
      this.shiftService.deleteShift(id).subscribe({
        next: response => {
          this.loadShifts();
          console.log('Shift deleted successfully', response);
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
          id:shift.id,
          busId:shift.busId,
          driverId:shift.driverId,
          shiftDate:shift.shiftDate,
          shiftStartingTime:shift.shiftStartingTime,
          shiftEndingTime:shift.shiftEndingTime
        },  
      });
      dialogRef.afterClosed().subscribe(result => {
        this.loadShifts();
      });
    }
  cancel() {
    this.router.navigate(['/manager-dashboard/shifts']);
  }
  goToAddForm(){
    this.router.navigate(['/manager-dashboard/shifts/add']);
  }
  openShiftDetails(shift:Shift){
    this.router.navigate(['/manager-dashboard/shifts/details'],  { queryParams: shift });
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
