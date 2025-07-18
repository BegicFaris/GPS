import { Component, inject, Inject } from '@angular/core';
import { FormsModule, } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BusService } from '../../_services/bus.service';
import { Title } from '@angular/platform-browser';
import { LettersNumbersDashesValidatorDirective } from '../../validators/letters-numbers-dashes.validator';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-bus-edit',
  standalone: true,
  imports: [FormsModule, LettersNumbersDashesValidatorDirective ],
  templateUrl: './bus-edit.component.html',
  styleUrl: './bus-edit.component.css'
})
export class BusEditComponent {

  constructor(public dialogRef: MatDialogRef<BusEditComponent>, @Inject(MAT_DIALOG_DATA) public busUpdate: { id: number, registrationNumber: string, manufacturer: string, model: string, capacity: string, manufactureYear: string }) {

  }
  currentYear: number = new Date().getFullYear();
  private busService = inject(BusService);
  private titleService = inject(Title);

  ngOnInit(): void {
    this.titleService.setTitle("Update bus");
  }

  async saveChanges() {
    try{
      await firstValueFrom(this.busService.updateBus(this.busUpdate));
      this.dialogRef.close(this.busUpdate);
    }
    catch(err){
      console.error(err);
    }
  }
  
  closeDialog() {
    this.dialogRef.close();
  }

}

