import { Component, inject } from '@angular/core';
import { LineService } from '../../_services/line.service';
import { Line } from '../../_models/line';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { LineEditComponent } from '../line-edit/line-edit.component';
import { Station } from '../../_models/station';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-line-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule],
  templateUrl: './line-view.component.html',
  styleUrl: './line-view.component.css',
})
export class LineViewComponent {
  private lineService = inject(LineService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  lines: Line[] = [];

  ngOnInit() {
    this.titleService.setTitle('Lines');
    this.loadLines();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/lines') {
        this.loadLines();
      }
    });
  }
  loadLines() {
    this.lineService.getAllLines().subscribe((data) => {
      this.lines = data; // or data.lines if it's nested
      console.log(this.lines);
    });
  }
  deleteLine(id: number) {
    if (confirm('Are you sure you want to delete this line?')) {
      this.lineService.deleteLine(id).subscribe({
        next: (response) => {
          this.loadLines();
          console.log('Line deleted successfully', response);
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting line', error);
        },
      });
    }
  }
  openEditDialog(line: Line) {
    const dialogRef = this.dialog.open(LineEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: line.id,
        name: line.name,
        startingStationId: line.startingStationId,
        endingStationId: line.endingStationId,
        completeDistance: line.completeDistance,
        isActive: line.isActive,
      }, // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe((result) => {
      this.loadLines();
      if (result) {
        console.log('Updated Line:', result);
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/lines']);
  }
}
