import { Component, OnInit, Input } from '@angular/core';
import { Opportunity } from '../models/opportunityModel';
import { Router } from '@angular/router';
import { OpportunityService } from '../create-opportunity/opportunity.service';
import { DataService } from '../../shared/dataService';

@Component({
    selector: 'app-pipeline',
    templateUrl: 'pipline.component.html',
    styles: []
})
export class PipelineComponent implements OnInit {

    public customers = [];
    searchTerm: string;
     
    opportunities: Opportunity[];


    constructor(private data: DataService, private _opportunityService: OpportunityService, private _router: Router) { }

    ngOnInit() {
        this._opportunityService.getOpportunities().subscribe(opportunities => this.opportunities = opportunities);
        this.displayAllCustomers();

    }

    displayAllCustomers(): void {

        this.data.loadAllCustomers()
            .subscribe(success => {
                if (success) {
                    this.customers = this.data.customers;
                }
            })
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
