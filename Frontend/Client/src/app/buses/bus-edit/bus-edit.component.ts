import { Component, inject, Inject } from '@angular/core';
import { FormsModule, } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BusService } from '../../_services/bus.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-bus-edit',
  standalone: true,
  imports: [FormsModule, ],
  templateUrl: './bus-edit.component.html',
  styleUrl: './bus-edit.component.css'
})
export class BusEditComponent {

  constructor(public dialogRef: MatDialogRef<BusEditComponent>, @Inject(MAT_DIALOG_DATA) public busUpdate: { id: number, registrationNumber: string, manufacturer: string, model: string, capacity: string, manufactureYear: string }) {

  }
  private busService = inject(BusService);
  private titleService = inject(Title);

  ngOnInit(): void {
    this.titleService.setTitle("Update bus");
  }

  saveChanges() {
    this.busService.updateBus(this.busUpdate).subscribe({
      next: response => {
        console.log('Bus updated successfully', response);
      },
      error: error => {
        console.error('Error deleting bus', error);
      }
    });
    this.dialogRef.close(this.busUpdate);
  }
  closeDialog() {
    this.dialogRef.close();
  }

}

