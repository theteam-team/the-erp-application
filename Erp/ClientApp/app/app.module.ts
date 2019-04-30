import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'

import { AppComponent } from './app.component';
import { Login } from './login/login.component';
import { Accounting } from './accountingSystem';
import { ModuleList } from './services/moduleList.component';
import { AccountingModule } from './accounting/accounting';
import { DataService } from './shared/dataService';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes, Router } from '@angular/router';
import { WarehouseComponent } from './warehouse/warehouse.component';
import { ProductsComponent } from './warehouse/products/products.component';
import { OrdersComponent } from './warehouse/orders/orders.component';
import { ConfigurationComponent } from './warehouse/configuration/configuration.component';
import { ProductComponent } from './warehouse/product/product.component';
import { OrderComponent } from './warehouse/order/order.component';
import { CreateOpportunityComponent } from './CRM/create-opportunity/create-opportunity.component';
import { CreateCustomerComponent } from './CRM/create-customer/create-customer.component';
import { CustomersComponent } from './CRM/customers/customers.component';
import { PipelineComponent } from './CRM/pipeline/pipeline.component';
import { CrmComponent } from './CRM/crm.component';
import { customerService } from './crm/customers/customer.service';
import { CustomerFilterPipe } from './crm/customers/customer-filter';
import { OpportunityFilterPipe } from './crm/pipeline/opportunity-filter';

let routes = [

    { path: "", component: ModuleList }, 
    { path: "accounting", component: AccountingModule },

    { path: "", component: ModuleList },

    { path: "accounting", component: AccountingModule },
    { path: "login", component: Login },

    { path: "warehouse", component: WarehouseComponent },
    { path: "warehouse/products", component: ProductsComponent },
    { path: "warehouse/orders", component: OrdersComponent },
    { path: "warehouse/configuration", component: ConfigurationComponent },
    { path: "warehouse/products/:productid", component: ProductComponent },
    { path: "warehouse/orders/:orderid", component: OrderComponent },

    { path: "crm", component: CrmComponent },
    { path: "crm/pipeline", component:PipelineComponent },
    { path: "crm/customers", component: CustomersComponent },
    { path: "crm/editCustomer/:id", component: CreateCustomerComponent },
    { path: "crm/editOpportunity/:id", component: CreateOpportunityComponent }
];
@NgModule({
  declarations: [
      AppComponent,
      ModuleList,
      Accounting,
      AccountingModule,
      Login,
      WarehouseComponent,
      ProductsComponent,
      OrdersComponent,
      ConfigurationComponent,
      ProductComponent,
      OrderComponent,
      PipelineComponent,
      CustomersComponent,
      CreateCustomerComponent,
      CreateOpportunityComponent,
      CrmComponent,
      CustomerFilterPipe,
      OpportunityFilterPipe
    ],
  imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
    

      RouterModule.forRoot(routes, {
          useHash: true,
          enableTracing: false
      })
    ],

  providers: [
      DataService,
      customerService
    ],

    bootstrap: [AppComponent, Accounting, Login] // 
    
})
export class AppModule { }
 