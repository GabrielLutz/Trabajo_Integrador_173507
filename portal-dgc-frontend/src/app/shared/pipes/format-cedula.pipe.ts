import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatCedula'
})
export class FormatCedulaPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
