import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { customerService } from '../customers/customer.service';
import { Opportunity } from '../models/opportunityModel';
import { Customer } from '../models/customerModel';
import { NgForm } from '@angular/forms';
import { OpportunityService } from './opportunity.service';


@Component({
  selector: 'app-create-opportunity',
  templateUrl: './create-opportunity.component.html',
  styles: []
})
export class CreateOpportunityComponent implements OnInit {

  @ViewChild('opportunityForm') public createOpportunityForm: NgForm;
  Title: string;
  customers: Customer[];
  opportunity: Opportunity;



  constructor(private _customerService: customerService, private _opportunityService: OpportunityService,
    private _router: Router, private _route: ActivatedRoute) { }

  ngOnInit() {
    this._customerService.getCustomers().subscribe(customers => this.customers = customers);

    this._route.paramMap.subscribe(parameterMap => {
      const id = +parameterMap.get('id');
      this.getOpportunity(id);
    });
  }

  private getOpportunity(id: number) {
    if (id === 0) {
      this.opportunity = {
        id: null,
        title: null,
        customer_id: null,
        expected_revenue: null,
        status: 1,
        start_date: null,

      };
      this.Title = 'Create an Opportunity';
    }
    else {
      this.Title = 'Update Opportunity';
      this._opportunityService.getOpportunity(id).subscribe(
        (opportunity) => this.opportunity = opportunity,
        (err) => console.log(err)
      );
    }
  }

  saveOpportunity(): void {
    if (this.opportunity.id == null) {
      this._opportunityService.saveOpportunity(this.opportunity).subscribe(
        (data: Opportunity) => {
          console.log(data);
          this._router.navigate([''])
        }
      );
    }
    else {
      this._opportunityService.editOpportunity(this.opportunity).subscribe(
        ( ) => {
          this._router.navigate([''])
        }
      );
    }
}
}
