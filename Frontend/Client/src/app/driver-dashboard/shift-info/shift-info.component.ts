import { Component, OnInit } from '@angular/core';
import { ShiftDto, ShiftService } from '../../_services/shift.service';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../_services/auth.service';
import { AccountService } from '../../_services/account.service';
import { CommonModule, formatDate } from '@angular/common';
import { ShiftListComponent } from '../shift-list/shift-list.component';

@Component({
  selector: 'app-shift-info',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ShiftListComponent],
  templateUrl: './shift-info.component.html',
  styleUrl: './shift-info.component.scss'
})
export class ShiftInfoComponent implements OnInit{
  currentShifts: ShiftDto[] = []
  upcomingShifts: ShiftDto[] = []
  endedShifts: ShiftDto[] = []
  filterForm: FormGroup
  loading = false
  driverId: number = 0

  constructor(
    private shiftService: ShiftService,
    private authService: AccountService,
    private fb: FormBuilder,
  ) {
    this.filterForm = this.fb.group({
      fromDate: [null],
      toDate: [null],
    })
  }

  ngOnInit(): void {
    // Get the current driver ID from auth service
    this.driverId = Number(this.authService.getUserId());
    this.loadAllShifts()
  }

  loadAllShifts(): void {
    this.loading = true

    // Load current shifts
    this.shiftService.getCurrentShifts(this.driverId).subscribe((shifts) => {
      this.currentShifts = shifts
      this.loading = false
    })

    // Load upcoming shifts
    this.shiftService.getUpcomingShifts(this.driverId).subscribe((shifts) => {
      this.upcomingShifts = shifts
    })

    // Load ended shifts
    this.shiftService.getEndedShifts(this.driverId).subscribe((shifts) => {
      this.endedShifts = shifts
    })
  }

  applyFilter(): void {
    let { fromDate, toDate } = this.filterForm.value;
    fromDate = fromDate ? new Date(fromDate):null;
    toDate = toDate ? new Date(toDate) : null;
    this.loading = true

    this.shiftService.getDriverShifts(this.driverId, fromDate, toDate).subscribe((shifts) => {
      // Categorize shifts based on status
      this.currentShifts = shifts.filter((s) => s.status === "Current")
      this.upcomingShifts = shifts.filter((s) => s.status === "Upcoming")
      this.endedShifts = shifts.filter((s) => s.status === "Ended")
      this.loading = false
    })
  }

  resetFilter(): void {
    this.filterForm.reset()
    this.loadAllShifts()
  }
}
