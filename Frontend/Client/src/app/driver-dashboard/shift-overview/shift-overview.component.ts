import { Component, OnInit } from '@angular/core';
import { ShiftDetailDto, ShiftDetailsDto, ShiftService } from '../../_services/shift.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule, Location } from '@angular/common';

@Component({
  selector: 'app-shift-overview',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './shift-overview.component.html',
  styleUrl: './shift-overview.component.css'
})
export class ShiftOverviewComponent implements OnInit {
  shiftDetails: ShiftDetailsDto | null = null
  loading = true
  error = false

  constructor(
    private route: ActivatedRoute,
    private shiftService: ShiftService,
    private location: Location,
  ) {}

  ngOnInit(): void {
    const shiftId = this.route.snapshot.paramMap.get("id")
    if (shiftId) {
      this.loadShiftDetails(+shiftId)
    }
  }

  loadShiftDetails(shiftId: number): void {
    this.loading = true
    this.shiftService.getShiftDetails(shiftId).subscribe({
      next: (details) => {
        this.shiftDetails = details
        this.loading = false
      },
      error: (err) => {
        console.error("Error loading shift details", err)
        this.error = true
        this.loading = false
      },
    })
  }

  downloadPdf(): void {
    if (!this.shiftDetails) return

    this.shiftService.downloadShiftPdf(this.shiftDetails.shift.id).subscribe((blob) => {
      const url = window.URL.createObjectURL(blob)
      const a = document.createElement("a")
      a.href = url
      a.download = `shift-report-${this.shiftDetails?.shift.id}.pdf`
      document.body.appendChild(a)
      a.click()
      window.URL.revokeObjectURL(url)
      a.remove()
    })
  }

  goBack(): void {
    this.location.back()
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

