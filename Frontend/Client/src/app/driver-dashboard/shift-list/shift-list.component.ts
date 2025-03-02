import { Component, Input } from '@angular/core';
import { ShiftDto, ShiftService } from '../../_services/shift.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-shift-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './shift-list.component.html',
  styleUrl: './shift-list.component.css'
})
export class ShiftListComponent {
  @Input() shifts: ShiftDto[] = []

  constructor(
    private router: Router,
    private shiftService: ShiftService,
  ) {}

  viewShiftDetails(shiftId: number): void {
    this.router.navigate(["/driver-dashboard/shift-overviews", shiftId])
  }

  downloadPdf(shiftId: number, event: Event): void {
    event.stopPropagation()
    this.shiftService.downloadShiftPdf(shiftId).subscribe((blob) => {
      const url = window.URL.createObjectURL(blob)
      const a = document.createElement("a")
      a.href = url
      a.download = `shift-report-${shiftId}.pdf`
      document.body.appendChild(a)
      a.click()
      window.URL.revokeObjectURL(url)
      a.remove()
    })
  }

  getStatusClass(status: string): string {
    switch (status) {
      case "Current":
        return "bg-green-100 text-green-800"
      case "Upcoming":
        return "bg-blue-100 text-blue-800"
      case "Ended":
        return "bg-gray-100 text-gray-800"
      default:
        return "bg-gray-100 text-gray-800"
    }
  }
}
