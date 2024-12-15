import { Component, inject, Inject } from '@angular/core';
import { FormsModule, NgForm, } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { LineService } from '../../_services/line.service';
import { NgIf } from '@angular/common';
import { StationService } from '../../_services/station.service';
import { Station } from '../../_models/station';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-line-edit',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './line-edit.component.html',
  styleUrl: './line-edit.component.css'
})
export class LineEditComponent {

  constructor(public dialogRef: MatDialogRef<LineEditComponent>, @Inject(MAT_DIALOG_DATA) public lineUpdate: { id: number, name: string, startingStationId: number, endingStationId: number, completeDistance: string, isActive: boolean }) {

  }
  private lineService = inject(LineService);
  private stationService = inject(StationService);
  private titleService = inject(Title);
  stations: Station[] = [];

  ngOnInit(): void {
    this.titleService.setTitle("Update line");
    this.loadStations();
  }
  saveChanges(updateLineForm: NgForm) {
    if(updateLineForm.valid){
      this.lineService.updateLine(this.lineUpdate).subscribe({
        next: response => {
          console.log('Line update successfully', response);
        }
      });
      this.dialogRef.close(this.lineUpdate);
    }
  }
  closeDialog() {
    this.dialogRef.close();
  }
  loadStations() {
    this.stationService.getAllStations().subscribe(
      (data) => {
        this.stations = data; // or data.lines if it's nested
      },
    );
  }
}

