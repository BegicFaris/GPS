import { Component, inject } from '@angular/core';
import { BusService } from '../../_services/bus.service';
import { Bus } from '../../_models/bus';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { BusEditComponent } from '../bus-edit/bus-edit.component';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-bus-view',
  standalone: true,
  imports: [RouterLink, MatDialogModule, MatButtonModule],
  templateUrl: './bus-view.component.html',
  styleUrl: './bus-view.component.css',
})
export class BusViewComponent {
  private busService = inject(BusService);
  private router = inject(Router);
  private dialog = inject(MatDialog);
  private titleService = inject(Title);
  buses: Bus[] = [];

  ngOnInit() {
    this.titleService.setTitle('buss');
    this.loadBuses();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/buses') {
        this.loadBuses();
      }
    });
  }
  loadBuses() {
    this.busService.getAllBuses().subscribe((data) => {
      this.buses = data; // or data.buss if it's nested
      console.log(this.buses)
    });
  }
  deleteBus(id: number) {
    if (confirm('Are you sure you want to delete this bus?')) {
      this.busService.deleteBus(id).subscribe({
        next: (response) => {
          this.loadBuses();
          console.log('Bus deleted successfully', response);
          this.cancel(); // Navigate back after successful deletion
        },
        error: (error) => {
          console.error('Error deleting bus', error);
        },
      });
    }
  }
  openEditDialog(bus: Bus) {
    const dialogRef = this.dialog.open(BusEditComponent, {
      height: '800px',
      width: '1000px', // Customize the width of the dialog
      data: {
        id: bus.id,
        registrationNumber: bus.registrationNumber,
        manufacturer: bus.manufacturer,
        model: bus.model,
        capacity: bus.capacity,
        manufactureYear: bus.manufactureYear,
      }, // Pass the current data to the dialog
    });
    dialogRef.afterClosed().subscribe((result) => {
      this.loadBuses();
      if (result) {
        console.log('Updated bus:', result);
      }
    });
  }
  cancel() {
    this.router.navigate(['manager-dashboard/buses']);
  }
}
