import { Component, OnInit } from '@angular/core';
import { OpportunityService } from '../../shared/opportunity.service';
import { NgForm } from '@angular/forms';
import { CustomerService } from '../../shared/customer.service';

@Component({
  selector: 'app-opportunity',
  templateUrl: './opportunity.component.html',
  styles: []
})
export class OpportunityComponent implements OnInit {

  constructor(private opportunityService: OpportunityService, private customerService: CustomerService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if(form = null)
    form.resetForm();
    this.opportunityService.opportunityData = {
      id: null,
      title: null,
      customer_id: 0,
      customer_name: null,
      employee_id: 0,
      expected_revenue: null,
      status: null,
      start_date: null,
      end_date: null,
    };
    this.opportunityService.customers = [];
  }
}
