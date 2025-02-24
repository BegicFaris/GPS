import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CreditCardService } from '../../_services/credit-card.service';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-creditCard-create',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './creditCard-create.component.html',
  styleUrl: './creditCard-create.component.css',
})
export class CreditCardCreateComponent {
  private router = inject(Router);
  private creditCardService = inject(CreditCardService);
  private titleService = inject(Title);




  ngOnInit() {
    this.titleService.setTitle('Add creditCard');
  }
  creditCardCreate: any = {};
  addNewCreditCard(newCreditCardForm: NgForm) {

    if (newCreditCardForm.valid) {
      if (this.creditCardCreate.isActive === undefined) {
        this.creditCardCreate.isActive = false;
      }
      this.creditCardService.createCreditCard(this.creditCardCreate).subscribe({
        next: (response) => {
          console.log(response);
          this.cancel();
        },
      });
      this.router.navigate(['/manager-dashboard/creditCards']);
    }
    else{
      Object.keys(newCreditCardForm.controls).forEach(field => {
        const control = newCreditCardForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }
  cancel() {
    this.router.navigate(['/manager-dashboard/creditCards']);
  }

  minExpirationDate: string;

  constructor() {
    this.minExpirationDate = this.calculateMinExpirationDate();
  }

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

