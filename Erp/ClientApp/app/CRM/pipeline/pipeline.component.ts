import { Component, OnInit, Input } from '@angular/core';
import { Opportunity } from '../models/opportunityModel';
import { customerService } from '../customers/customer.service';
import { Router } from '@angular/router';
 

@Component({
  selector: 'app-pipeline',
  templateUrl: './pipeline.component.html',
  styles: []
})
export class PipelineComponent implements OnInit {
  searchTerm: string;
  private opportunities: Opportunity[];

  constructor(private _customerService: customerService , private _router:Router ) { }

  ngOnInit() {
    this._customerService.getOpportunities().subscribe( opportunity => this.opportunities= opportunity);
  }

  editOpportunity(opportunityID: number) {
    this._router.navigate(['/editOpportunity', opportunityID])
  }
  deleteOpportunity(opportunityID: number) {
    this._customerService.deleteOpportunity(opportunityID);
  }
}
