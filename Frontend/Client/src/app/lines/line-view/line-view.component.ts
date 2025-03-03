import { Component, inject } from '@angular/core';
import { LineService } from '../../_services/line.service';
import { Line } from '../../_models/line';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { LineEditComponent } from '../line-edit/line-edit.component';
import { Station } from '../../_models/station';
import { Title } from '@angular/platform-browser';
import { firstValueFrom } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

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
  private snackBar= inject(MatSnackBar);


  async ngOnInit() {
    await this.titleService.setTitle('Lines');
    this.loadLines();
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd && event.url === '/manager-dashboard/lines') {
        await this.loadLines();
      }
    });
  }
  async loadLines() {
    try{
      this.lines = await firstValueFrom(this.lineService.getAllLines());
    }
    catch(err){
      console.error(err);
    }
  }
  deleteLine(id: number) {
    if (confirm('Are you sure you want to delete this line?')) {
      this.lineService.deleteLine(id).subscribe({
        next: async (response) => {
          await this.loadLines();
          this.snackBar.open('Line deleted sucessfully', 'Ok', {
            duration: 4000, // Keep it visible for 4 seconds
            verticalPosition: 'top', // Show at the top
            horizontalPosition: 'center' // Centered horizontally
          });
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
      width: '1000px', 
      data: {
        id: line.id,
        name: line.name,
        completeDistance: line.completeDistance,
        isActive: line.isActive,
      },
      autoFocus: true,
      restoreFocus: true, 
    });
    dialogRef.afterClosed().subscribe(async (result) => {
      await  this.loadLines();
      if (result) {
        this.snackBar.open('Line updated sucessfully', 'Ok', {
          duration: 4000, // Keep it visible for 4 seconds
          verticalPosition: 'top', // Show at the top
          horizontalPosition: 'center' // Centered horizontally
        });
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/lines']);
  }
}
