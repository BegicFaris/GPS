import { Directive, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[appValidDate]',
  standalone: true,
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: DateValidatorDirective,
      multi: true,
    },
  ],
})
export class DateValidatorDirective implements Validator {
  @Input() minDate: string = '2000-01-01'; // Default minimum date
  @Input() maxDate: string = new Date().toISOString().split('T')[0]; // Default max: today

  validate(control: AbstractControl): ValidationErrors | null {
    if (!control.value) return null; // Let "required" handle empty values

    const enteredDate = new Date(control.value);
    const min = new Date(this.minDate);
    const max = new Date(this.maxDate);

    if (isNaN(enteredDate.getTime())) {
      return { invalidDate: 'Invalid date format' };
    }
    if (enteredDate < min) {
      return { minDate: `Date must be on or after ${this.minDate}` };
    }
    if (enteredDate > max) {
      return { maxDate: `Date cannot be in the future` };
    }

    return null; // No errors
  }
}