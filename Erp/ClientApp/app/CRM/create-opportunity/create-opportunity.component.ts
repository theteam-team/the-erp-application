import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { customerService } from '../customers/customer.service';
import { Opportunity } from '../models/opportunityModel';


@Component({
  selector: 'app-create-opportunity',
  templateUrl: './create-opportunity.component.html',
  styles: []
})
export class CreateOpportunityComponent implements OnInit {
  start_date: Date;
  opportunity: Opportunity;

  constructor(private _customerService: customerService, private _router: Router, private _route: ActivatedRoute) { }

  ngOnInit() {
    this._route.paramMap.subscribe(prameterMap => {
      const id = +prameterMap.get('id');
      this.getOpportunity(id);
    })
  }

  private getOpportunity(id: number) {
    if (id === 0) {
      this.opportunity = {
        id: null,
        title: null,
        customer_id: null,
        customer_name: null,
        employee_id: null,
        expected_revenue: null,
        status: null,
        start_date: null,
        end_date: null,
      }
    }
    else {
      this.opportunity = this._customerService.getOpportunity(id);
    }
  }

  saveOpportunity(): void {
    const newOpportunity: Opportunity = Object.assign({}, this.opportunity);
    this._customerService.saveOpportunity(newOpportunity);
    
    this._router.navigate([''])
  }
   
}
