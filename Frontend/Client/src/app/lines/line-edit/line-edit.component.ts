import { ChangeDetectorRef, Component, inject, Inject } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Line } from '../../_models/lines/line';
import { LineService } from '../../_services/line.service';
import { timeout } from 'rxjs';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-line-edit',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './line-edit.component.html',
  styleUrl: './line-edit.component.css'
})
export class LineEditComponent {

  constructor(public dialogRef: MatDialogRef<LineEditComponent>, @Inject(MAT_DIALOG_DATA) public data: any) {

  }
  private cdr = inject(ChangeDetectorRef);
  private lineService = inject(LineService);
  lineUpdate:any={};
  ngOnInit(): void {
    this.lineUpdate=this.data;
    this.Log();
    this.cdr.detectChanges();
  }
  Log() {
    console.log(this.lineUpdate);
  }
  saveChanges() {
    this.dialogRef.close(this.lineUpdate);
  }
  closeDialog() {
    this.dialogRef.close(this.lineUpdate);
  }
}

