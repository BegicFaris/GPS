import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currencyFormat',
  standalone: true
})
export class CurrencyFormatPipe implements PipeTransform {

  transform(value: number, currencyCode: string = 'KM', decimal: string = '.2-2'): string {
    const formattedValue = new Intl.NumberFormat('bs-BA', {
      style: 'currency',
      currency: 'BAM',
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    }).format(value);

    // Adjust to place the currency code after the value
    return formattedValue.replace(/^.*\s/, '').concat(` ${currencyCode}`);
  }

}
