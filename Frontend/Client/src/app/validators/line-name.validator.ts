import { Directive, forwardRef } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl, ValidationErrors } from '@angular/forms';

@Directive({
  selector: '[lineName]',
  standalone: true,
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => LineNameValidatorDirective),
      multi: true
    }
  ]
})
export class LineNameValidatorDirective implements Validator {
  private lineNamePattern = /^\d+:\s+[\p{L}]+(?:\s+[\p{L}]+)*\s*-\s*[\p{L}]+(?:\s+[\p{L}]+)*$/u;

  validate(control: AbstractControl): ValidationErrors | null {
    if (!control.value) {
      return null; // Let the required validator handle empty cases
    }
    return this.lineNamePattern.test(control.value) ? null : { lineName: true };
  }
}
