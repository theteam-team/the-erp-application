import { PipeTransform ,Pipe} from '@angular/core';
import { Opportunity } from '../models/opportunityModel';

 @Pipe({
   name:'opportunityFilter'
 })

export class OpportunityFilterPipe implements PipeTransform {
  transform(opportunity: Opportunity[], searchTerm: string): Opportunity[] {

    if (!opportunity || !searchTerm) {
      return opportunity;
    }
    return opportunity.filter(opportunity =>
      opportunity.customer_name.toLowerCase().indexOf(searchTerm.toLowerCase()) !==-1);
  }
}
