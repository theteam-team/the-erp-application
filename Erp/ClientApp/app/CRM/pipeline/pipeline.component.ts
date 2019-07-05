import { Component, OnInit, Input } from '@angular/core';
import { Opportunity } from '../models/opportunityModel';
import { customerService } from '../customers/customer.service';
import { Router } from '@angular/router';
 
import { OpportunityService } from '../create-opportunity/opportunity.service';
import { Customer } from '../models/customerModel';


@Component({
    selector: 'app-pipeline',
    templateUrl: 'pipline.component.html',
    styles: []
})
export class PipelineComponent implements OnInit {
    searchTerm: string;
    customers: Customer[];
    opportunities: Opportunity[];


    constructor(private _customerService: customerService, private _opportunityService: OpportunityService, private _router: Router) { }

    ngOnInit() {
        this._opportunityService.getOpportunities().subscribe(opportunities => this.opportunities = opportunities);
        this._customerService.getCustomers().subscribe(customers => this.customers = customers);

    }
    editOpportunity(opportunityID: number) {
        this._router.navigate(['/crm/editOpportunity', opportunityID])
    }

    deleteOpportunity(opportunityID: number) {

        this._opportunityService.deleteOpportunity(opportunityID).subscribe(
            () => console.log('Opportunity deleted'),
            (err) => console.log(err)
        );
        this._opportunityService.lossOpportunity();
    }



   

    totalNew() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 1) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }

    totalQualified() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 2) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }

    totalProposition() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 3) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }
    totalNegotiation() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 4) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }

    totalWon() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 5) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }
}
/*

  }
ngOnInit() {

}





*/
