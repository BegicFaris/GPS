import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { DiscountService } from '../../_services/discount.service';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { LettersNumbersValidatorDirective } from '../../validators/letters-numbers.validator';

@Component({
  selector: 'app-discount-create',
  standalone: true,
  imports: [FormsModule,LettersNumbersValidatorDirective, CommonModule],
  templateUrl: './discount-create.component.html',
  styleUrls: ['./discount-create.component.css'],
})
export class DiscountCreateComponent {
  private router = inject(Router);
  private discountService = inject(DiscountService);
  private titleService = inject(Title);

  discountCreate: { id: number, discountName: string; discountValue: number } = {
    id: 0,
    discountName: '',
    discountValue: 0,
  };

  ngOnInit() {
    this.titleService.setTitle('Add discount');
  }

  addNewDiscount(newDiscountForm: NgForm) {
    if (newDiscountForm.valid) {
      this.discountService.createDiscount(this.discountCreate).subscribe({
        next: (response) => {
          console.log('Discount created successfully:', response);
          this.router.navigate(['/manager-dashboard/discounts']);
        },
        error: (error) => {
          console.error('Error creating discount:', error);
        },
      });
    } else {
      Object.keys(newDiscountForm.controls).forEach((field) => {
        const control = newDiscountForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

  cancel() {
    this.router.navigate(['/manager-dashboard/discounts']);
  }
}
