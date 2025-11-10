import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatCedula',
  standalone: false
})
export class FormatCedulaPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) {
      return '';
    }

    const numeros = value.replace(/\D/g, '');

    if (numeros.length >= 7) {
      return `${numeros.substring(0, 1)}.${numeros.substring(1, 4)}.${numeros.substring(4, 7)}-${numeros.substring(7, 8)}`;
    }

    return value;
  }
}
