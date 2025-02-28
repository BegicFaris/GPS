import { Directive } from "@angular/core";
import { NG_VALIDATORS, Validator, AbstractControl, ValidationErrors } from "@angular/forms";


@Directive({
    selector: '[lettersNumbersDashes]',
    providers: [
        {
          provide: NG_VALIDATORS,
          useExisting: LettersNumbersDashesValidatorDirective,
          multi: true,
        },
    ],
    standalone: true,
})
export class LettersNumbersDashesValidatorDirective implements Validator {
    validate(control: AbstractControl): ValidationErrors | null {
      const regex = /^[\p{L}\p{N}\- ]+$/u; // Unicode letters, numbers, and spaces
      return regex.test(control.value) ? null : { lettersNumbersDashes: true };
    }
}