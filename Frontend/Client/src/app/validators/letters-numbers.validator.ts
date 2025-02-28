import { Directive } from "@angular/core";
import { NG_VALIDATORS, Validator, AbstractControl, ValidationErrors } from "@angular/forms";


@Directive({
    selector: '[lettersNumbers]',
    providers: [
        {
          provide: NG_VALIDATORS,
          useExisting: LettersNumbersValidatorDirective,
          multi: true,
        },
    ],
    standalone: true,
})
export class LettersNumbersValidatorDirective implements Validator {
    validate(control: AbstractControl): ValidationErrors | null {
      const regex = /^[\p{L}\p{N} ]+$/u; // Unicode letters, numbers, and spaces
      return regex.test(control.value) ? null : { lettersNumbers: true };
    }
}