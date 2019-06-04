import { Injectable } from '@angular/core';
import { Opportunity } from './opportunity.model';
import { Customer } from './customer.model';

@Injectable({
  providedIn: 'root'
})
export class OpportunityService {

  opportunityData: Opportunity;
  customers: Customer[];

  constructor() { }
}
