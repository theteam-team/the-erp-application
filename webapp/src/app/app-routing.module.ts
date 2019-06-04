import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'; 
import { CustomersComponent } from './crm/customers/customers.component';
import { PipelineComponent } from './crm/pipeline/pipeline.component';
import { CreateOpportunityComponent } from './crm/create-opportunity/create-opportunity.component';
import { CreateCustomerComponent } from './crm/create-customer/create-customer.component';
import {  OpportunityCanDeactivateGuardService } from './crm/create-opportunity/opportunity-canDeactivate-guard-service';
import { CustomerCanDeactivateGuardService } from './crm/create-customer/customer-canDeactivate-guard-service';
 
 

const routes: Routes = [
  
  { path: '', component: PipelineComponent },
  { path: 'customers', component: CustomersComponent },
  {
    path: 'editCustomer/:id',
    component: CreateCustomerComponent,
    canDeactivate:[CustomerCanDeactivateGuardService]
  },
  {
    path: 'editOpportunity/:id',
    component: CreateOpportunityComponent,
    canDeactivate: [OpportunityCanDeactivateGuardService]
  },


   ];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
