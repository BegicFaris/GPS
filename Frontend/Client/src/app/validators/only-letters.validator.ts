import { Directive } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, Validator, ValidationErrors } from '@angular/forms';

@Directive({
  selector: '[lettersOnly]',
  standalone: true, // ✅ Standalone directive
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: LettersOnlyValidatorDirective,
      multi: true,
    },
  ],
})
export class LettersOnlyValidatorDirective implements Validator {
  validate(control: AbstractControl): ValidationErrors | null {
    if (!control.value) return null; 

    const regex = /^[\p{L} ]+$/u; // 
    return regex.test(control.value) ? null : { lettersOnly: true };
  }
}