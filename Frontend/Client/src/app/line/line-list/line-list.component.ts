import { Component, OnInit } from '@angular/core';
import { Line } from '../line.model';  // Change from Bus to Line
import { LineService } from '../line.service';  // Change service to LineService
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-line-list',
  templateUrl: './line-list.component.html',  // Ensure the template is updated to reflect 'Line'
  styleUrls: ['./line-list.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class LineListComponent implements OnInit {
  lines: Line[] = [];  // Changed from 'buses' to 'lines'
  selectedLine: Line | null = null;  // Changed from 'selectedBus' to 'selectedLine'

  constructor(private lineService: LineService) { }

  ngOnInit(): void {
    this.loadLines();  // Changed method to load lines instead of buses
  }

  loadLines(): void {
    this.lineService.getAllLines().subscribe((data: Line[]) => {
      this.lines = data;  // Assign the data fetched from service to 'lines'
    });
  }

  selectLine(line: Line): void {
    this.selectedLine = { ...line };  // Make a copy of the selected line for editing
  }

  deleteLine(id: number): void {
    this.lineService.deleteLine(id).subscribe(() => {
      this.loadLines();  // Reload lines after deletion
    });
  }

  // Edit a line (update line details)
  editLine(): void {
    if (this.selectedLine) {
      this.lineService.updateLine(this.selectedLine).subscribe(() => {
        this.loadLines();  // Reload lines after editing
        this.selectedLine = null;  // Clear the selected line after update
      });
    }
  }

  resetSelectedLine(): void {
    this.selectedLine = null;  // Reset the selected line
  }

  // Cancel editing
  cancelEdit(): void {
    this.selectedLine = null;  // Reset without saving changes
  }

  // TrackBy function to optimize rendering performance in *ngFor
  trackById(index: number, line: Line): number {
    return line.id;  // Track by line id to ensure unique identification
  }
}
