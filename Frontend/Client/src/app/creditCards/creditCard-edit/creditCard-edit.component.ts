import { Component, inject, Inject } from '@angular/core';
import { FormsModule, } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreditCardService } from '../../_services/credit-card.service';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-creditCard-edit',
  standalone: true,
  imports: [FormsModule, ],
  templateUrl: './creditCard-edit.component.html',
  styleUrl: './creditCard-edit.component.css'
})
export class CreditCardEditComponent {

  constructor(public dialogRef: MatDialogRef<CreditCardEditComponent>, @Inject(MAT_DIALOG_DATA) public creditCardUpdate: { id: number, cardNumber: string, expirationDate: string, cardName: string, ccv: number }) {

  }
  private creditCardService = inject(CreditCardService);
  private titleService = inject(Title);

  ngOnInit(): void {
    this.titleService.setTitle("Update credit card");
  }

  saveChanges() {
    this.creditCardService.updateCreditCard(this.creditCardUpdate).subscribe({
      next: response => {
        console.log('Credit card updated successfully', response);
      },
      error: error => {
        console.error('Error deleting credit card', error);
      }
    });
    this.dialogRef.close(this.creditCardUpdate);
  }
  closeDialog() {
    this.dialogRef.close();
  }

  minExpirationDate: string=this.calculateMinExpirationDate();

  private calculateMinExpirationDate(): string {
    const today = new Date();
    const month = today.getMonth() + 1; // Months are 0-based in JS
    const year = today.getFullYear().toString().slice(-2); // Get last two digits of the current year
    return `${month < 10 ? '0' : ''}${month}/${year}`;
  }

  getMinExpirationDate(): string {
    return this.minExpirationDate;
  }

  // Luhn Algorithm for validating card number
  validateCardNumber(cardNumber: string): boolean {
    let sum = 0;
    let shouldDouble = false;

    // Loop over the digits in reverse order
    for (let i = cardNumber.length - 1; i >= 0; i--) {
      let digit = parseInt(cardNumber.charAt(i), 10);
      if (shouldDouble) {
        digit *= 2;
        if (digit > 9) {
          digit -= 9;
        }
      }
      sum += digit;
      shouldDouble = !shouldDouble;
    }
    return sum % 10 === 0;
  }

  checkCardNumberValidity(cardNumber: string): boolean {
    return this.validateCardNumber(cardNumber);
  }
}

