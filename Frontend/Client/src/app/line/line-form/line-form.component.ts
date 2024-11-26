import { Component, OnInit } from '@angular/core';
import { LineDTO } from '../line-dto.model';  // LineDTO interface for form
import { Line } from '../line.model';  // Full Line model for handling the complete object
import { LineService } from '../line.service';  // Service for backend interaction
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';  // Import FormsModule

@Component({
  selector: 'app-line-form',
  standalone: true,
  imports: [FormsModule], // Add FormsModule here
  templateUrl: './line-form.component.html',
  styleUrls: ['./line-form.component.css']
})
export class LineFormComponent implements OnInit {
  // Using LineDTO for form submission (without ID)
  line: LineDTO = {
    name: '',
    startingStationId: 0,
    endingStationId: 0,
    completeDistance: '',
    isActive: true
  };

  constructor(
    private lineService: LineService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      // Fetch the line by ID for editing (this gives the full Line object including ID)
      this.lineService.getLineById(Number(id)).subscribe((line: Line) => {
        // For editing, use the LineDTO object, but fill it with the values from Line
        this.line = {
          name: line.name,
          startingStationId: line.startingStationId,
          endingStationId: line.endingStationId,
          completeDistance: line.completeDistance,
          isActive: line.isActive
        };
      });
    }
  }

  saveLine(): void {
    const lineToSend: LineDTO = {
      name: this.line.name,
      startingStationId: this.line.startingStationId,
      endingStationId: this.line.endingStationId,
      completeDistance: this.line.completeDistance,
      isActive: this.line.isActive
    };

    if ((this.line as Line).id) {
      // If it's an existing line (with ID), update the line (should not happen here if no ID)
      const fullLine: Line = this.line as Line;
      this.lineService.updateLine(fullLine).subscribe(() => {
        this.router.navigate(['/']); // Navigate after successful update
      });
    } else {
      // Otherwise, create a new line (send LineDTO to backend)
      this.lineService.createLine(lineToSend).subscribe(() => {
        this.router.navigate(['/']); // Navigate after successful creation
      });
    }
  }
}
