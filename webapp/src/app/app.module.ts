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
 
import { CustomerFilterPipe } from './crm/customers/customer-filter';
import { OpportunityFilterPipe } from './crm/pipeline/opportunity-filter';
import { HttpClientModule } from '@angular/common/http';
 
 



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

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    HttpClientModule,
   
    
  ],
  providers: [customerService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
