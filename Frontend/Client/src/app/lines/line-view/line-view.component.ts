import { Component, inject } from '@angular/core';
import { LineService } from '../../_services/line.service';
import { Line } from '../../_models/line';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { LineEditComponent } from '../line-edit/line-edit.component';

@Component({
  selector: 'app-line-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule],
  templateUrl: './line-view.component.html',
  styleUrl: './line-view.component.css'
})
export class LineViewComponent {
  private lineService = inject(LineService);
  private router = inject(Router);
  private dialog = inject(MatDialog)
  lines: Line[] = [];

  ngOnInit() {
    this.loadLines();
    // Reload data on navigation back
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/lines') {
        this.loadLines();
      }
    });
  }
  loadLines() {
    this.lineService.getAllLines().subscribe(
      (data) => {
        this.lines = data; // or data.lines if it's nested
      },
    );
  }
  deleteLine(id: number) {
    if (confirm('Are you sure you want to delete this line?')) {
      this.lineService.deleteLine(id).subscribe({
        next: response => {
          this.loadLines();
          console.log('Line deleted successfully', response);
          this.cancle(); // Navigate back after successful deletion
        },
        error: error => {
          console.error('Error deleting line', error);
        }
      });
    }

  }
  openEditDialog(line: Line) {
    const dialogRef = this.dialog.open(LineEditComponent, {
      height: '800px',
      width: '1000px',  // Customize the width of the dialog
      data: { line },  // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadLines();
        console.log('Updated Line:', result);
      }
    });

  }
  cancle() {
    this.router.navigate(['/lines']);
  }
}
