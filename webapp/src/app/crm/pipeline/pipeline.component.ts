import { Component, OnInit, Input } from '@angular/core';
import { Opportunity } from '../models/opportunityModel';
import { customerService } from '../services/customer.service';
import { Router } from '@angular/router';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { OpportunityService } from '../services/opportunity.service';
import { Customer } from '../models/customerModel';
 

@Component({
  selector: 'app-pipeline',
  templateUrl: './pipeline.component.html',
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
    this._router.navigate(['/editOpportunity', opportunityID])
  }

  deleteOpportunity(opportunityID: number) {
   
    this._opportunityService.deleteOpportunity(opportunityID).subscribe(
      () => console.log('Opportunity deleted'),
      (err) => console.log(err)
    );
    this._opportunityService.lossOpportunity();
  }

 

  drop(event: CdkDragDrop<Opportunity[]>) {
    if (event.previousContainer !== event.container) {
      transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
    } else {
      moveItemInArray(this.opportunities, event.previousIndex, event.currentIndex);
    }
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
