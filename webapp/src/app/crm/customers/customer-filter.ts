import { PipeTransform, Pipe } from '@angular/core';
import { Customer } from '../models/customerModel';

 @Pipe({
   name:'customerFilter'
 })

export class CustomerFilterPipe implements PipeTransform {
  transform(customers: Customer[], searchTerm: string): Customer[] {

    if (!customers || !searchTerm) {
      return customers;
    }
    return customers.filter(customer =>
      customer.first_name.toLowerCase().indexOf(searchTerm.toLowerCase()) !==-1);
  }
}
