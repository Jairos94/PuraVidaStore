import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'pipePrincipal'
})
export class PipePrincipalPipe implements PipeTransform {


  DateT: Date = new Date();

  transform(name: string): string {
    let hi: string = '';
    if (this.DateT.getHours() > 0 && this.DateT.getHours() < 11) {
      hi = `Buenos días, ${name}`;
    }
    else {
      if (this.DateT.getHours() >= 12 && this.DateT.getHours() < 18) {
        hi = `Buenas tardes, ${name}`;
      }
      else {
        hi = `Buenas noches, ${name}`;
      }
    }

    return hi;
  }


}
