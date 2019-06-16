import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule} from '@angular/forms'

import { MatCardModule, MatButtonModule } from '@angular/material';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CrmComponent } from './crm/crm.component';
import { CustomersComponent } from './crm/customers/customers.component';
import { PipelineComponent } from './crm/pipeline/pipeline.component';
import { CreateOpportunityComponent } from './crm/create-opportunity/create-opportunity.component';
import { CreateCustomerComponent } from './crm/create-customer/create-customer.component';
import { customerService } from './crm/customers/customer.service';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CustomerFilterPipe } from './crm/customers/customer-filter';
import { OpportunityFilterPipe } from './crm/pipeline/opportunity-filter';
import { HttpClientModule } from '@angular/common/http';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OpportunityService } from './crm/services/opportunity.service';
import {  OpportunityCanDeactivateGuardService } from './crm/create-opportunity/opportunity-canDeactivate-guard-service';
import { CustomerCanDeactivateGuardService } from './crm/create-customer/customer-canDeactivate-guard-service';
import { QoutationComponent } from './crm/qoutation/qoutation.component';
import { CreateQoutationComponent } from './crm/create-qoutation/create-qoutation.component';
import { MatDialogModule } from '@angular/material/dialog';
import { SalesmanService } from './crm/qoutation/salesman.service';
import { ProductService } from './crm/qoutation/product.service';
import { QoutationService } from './crm/qoutation/qoutation.service';
import { DashboardComponent } from './crm/dashboard/dashboard.component';
import { ChartsModule } from 'ng2-charts';
 
 



@NgModule({
  declarations: [
    AppComponent,
    CrmComponent,
    CustomersComponent,
    PipelineComponent,
    CreateOpportunityComponent,
    CreateCustomerComponent,
    CustomerFilterPipe,
    OpportunityFilterPipe,
    QoutationComponent,
    CreateQoutationComponent,
    DashboardComponent,
    
    
     

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    HttpClientModule,
    DragDropModule,
    BrowserAnimationsModule,
    MatDialogModule,
    ChartsModule
   
    
  ],
  providers: [customerService,
              OpportunityService,
              OpportunityCanDeactivateGuardService,
              CustomerCanDeactivateGuardService,
              SalesmanService,
              ProductService,
              QoutationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
