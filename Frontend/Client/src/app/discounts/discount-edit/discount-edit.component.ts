import { Component, inject, Inject } from '@angular/core';
import { FormsModule, } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DiscountService } from '../../_services/discount.service';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { LettersNumbersValidatorDirective } from '../../validators/letters-numbers.validator';

@Component({
  selector: 'app-discount-edit',
  standalone: true,
  imports: [FormsModule, LettersNumbersValidatorDirective,CommonModule],
  templateUrl: './discount-edit.component.html',
  styleUrl: './discount-edit.component.css'
})
export class DiscountEditComponent {

  constructor(public dialogRef: MatDialogRef<DiscountEditComponent>, @Inject(MAT_DIALOG_DATA) public discountUpdate: { id: number, discountName: string, discountValue: string }) {

  }
  private discountService = inject(DiscountService);
  private titleService = inject(Title);

  ngOnInit(): void {
    this.titleService.setTitle("Update discount");
  }

  saveChanges() {
    this.discountService.updateDiscount(this.discountUpdate).subscribe({
      next: response => {
        console.log('Discount updated successfully', response);
      },
      error: error => {
        console.error('Error deleting discount', error);
      }
    });
    this.dialogRef.close(this.discountUpdate);
  }
  closeDialog() {
    this.dialogRef.close();
  }

}

