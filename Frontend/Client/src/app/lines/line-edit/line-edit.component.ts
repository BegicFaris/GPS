import { Component, inject, Inject } from '@angular/core';
import { FormsModule,  } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { LineService } from '../../_services/line.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-line-edit',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './line-edit.component.html',
  styleUrl: './line-edit.component.css'
})
export class LineEditComponent {

  constructor(public dialogRef: MatDialogRef<LineEditComponent>, @Inject(MAT_DIALOG_DATA) public lineUpdate: {id:number, name:string, startingStationId : number, endingStationId:number,completeDistance:string, isActive:boolean }) {

  }
  private lineService = inject(LineService);
  ngOnInit(): void {
    this.Log();
  }
  Log() {
    console.log(this.lineUpdate);
  }
  saveChanges() {
    this.lineService.updateLine(this.lineUpdate).subscribe({
      next: response => {
        console.log('Line update successfully', response);
      },
      error: error => {
        console.error('Error deleting line', error);
      }
    });
    this.dialogRef.close(this.lineUpdate);
  }
  closeDialog() {
    this.dialogRef.close();
  }
}

