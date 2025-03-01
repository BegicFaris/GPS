import { Component, inject, Inject } from '@angular/core';
import { FormsModule, NgForm, } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { LineService } from '../../_services/line.service';
import { NgIf } from '@angular/common';
import { StationService } from '../../_services/station.service';
import { Station } from '../../_models/station';
import { Title } from '@angular/platform-browser';
import { LineNameValidatorDirective } from '../../validators/line-name.validator';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-line-edit',
  standalone: true,
  imports: [FormsModule,LineNameValidatorDirective],
  templateUrl: './line-edit.component.html',
  styleUrl: './line-edit.component.css'
})
export class LineEditComponent {

  constructor(public dialogRef: MatDialogRef<LineEditComponent>, @Inject(MAT_DIALOG_DATA) public lineUpdate: { id: number, name: string, startingStationId: number, endingStationId: number, completeDistance: string, isActive: boolean }) {

  }
  private lineService = inject(LineService);
  private titleService = inject(Title);

  ngOnInit(): void {
    this.titleService.setTitle("Update line");
  }
  async saveChanges(updateLineForm: NgForm) {
    if(updateLineForm.valid){
      try {
        await firstValueFrom(this.lineService.updateLine(this.lineUpdate));
        this.dialogRef.close(this.lineUpdate);
      }
      catch (err) {
        console.error(err);
      }
    }
  }
  closeDialog() {
    this.dialogRef.close();
  }
  
}

