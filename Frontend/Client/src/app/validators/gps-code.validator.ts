import { Directive } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[appGpsCodeValidator]',
  standalone: true,
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: GpsCodeValidatorDirective,
      multi: true,
    },
  ],
})
export class GpsCodeValidatorDirective implements Validator {
  private gpsPattern =
    /^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$/;

  validate(control: AbstractControl): ValidationErrors | null {
    if (!control.value) return null; // Let "required" handle empty values

    return this.gpsPattern.test(control.value)
      ? null
      : { invalidGpsCode: 'GPS Code must be in the correct format (e.g., 1.12, 9.78).' };
  }
}
