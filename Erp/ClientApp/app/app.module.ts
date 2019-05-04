import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { MatCardModule, MatButtonModule } from '@angular/material';
import { AppComponent } from './app.component';
import { Login } from './login/login.component';
import { Accounting } from './accountingSystem';
import { ModuleList } from './services/moduleList.component';
import { AccountingModule } from './accounting/accounting';
import { AccountingBody } from './accounting/body/bodycomponent';
import { AccountingSummary } from './accounting/body/summary/summary.component';
import { AccountingActivity } from './accounting/body/activity/activity.component';
import { AccountingSendRequest } from './accounting/body/send-request/send-request.component';
import { AccountingWallet } from './accounting/body/wallet/wallet.component'; 
import { AccountingOffers } from './accounting/body/offers/offers.component'; 
import { AccountingHelp } from './accounting/body/help/help.component';
import { AccountingSend } from './accounting/body/send-request/send/send.component';
import { Invoice } from './accounting/body/send-request/send/createInvoice/invoice.component';
import { DataService } from './shared/dataService';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes, Router } from '@angular/router';

import { WarehouseComponent } from './warehouse/warehouse.component';
import { ProductsComponent } from './warehouse/products/products.component';
import { OrdersComponent } from './warehouse/orders/orders.component';
import { ConfigurationComponent } from './warehouse/configuration/configuration.component';
import { ProductComponent } from './warehouse/product/product.component';
import { OrderComponent } from './warehouse/order/order.component';
import { AddProductComponent } from './warehouse/add-product/add-product.component';
import { AddOrderComponent } from './warehouse/add-order/add-order.component';

import { CreateOpportunityComponent } from './CRM/create-opportunity/create-opportunity.component';
import { CreateCustomerComponent } from './CRM/create-customer/create-customer.component';
import { CustomersComponent } from './CRM/customers/customers.component';
import { PipelineComponent } from './CRM/pipeline/pipeline.component';
import { CrmComponent } from './CRM/crm.component';
import { customerService } from './crm/customers/customer.service';
import { CustomerFilterPipe } from './crm/customers/customer-filter';
import { OpportunityFilterPipe } from './crm/pipeline/opportunity-filter';
import { AddProductsInOrderComponent } from './warehouse/add-products-in-order/add-products-in-order.component';

let routes = [

    { path: "", component: ModuleList }, 
    { path: "accounting", component: AccountingModule },
    { path: "", component: ModuleList },
    { path: "login", component: Login },

    { path: "warehouse", component: WarehouseComponent },
    { path: "warehouse/products", component: ProductsComponent },
    { path: "warehouse/orders", component: OrdersComponent },
    { path: "warehouse/configuration", component: ConfigurationComponent },
    { path: "warehouse/product/:productid", component: ProductComponent },
    { path: "warehouse/order/:orderid", component: OrderComponent },
    { path: "warehouse/addproduct", component: AddProductComponent },
    { path: "warehouse/addOrder", component: AddOrderComponent },
    { path: "warehouse/addOrder/addProducts", component: AddProductsInOrderComponent },

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
      AccountingBody,
      AccountingSummary,
      AccountingActivity,
      AccountingSendRequest,
      AccountingWallet,
      AccountingOffers,
      AccountingHelp,
      AccountingSend,
      Invoice,
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
      OpportunityFilterPipe,
      AddProductComponent,
      AddOrderComponent,
      AddProductsInOrderComponent
    ],
  imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      MatCardModule,
      MatButtonModule,   

      RouterModule.forRoot(routes, {
          useHash: true,
          enableTracing: false,
          onSameUrlNavigation: "reload"
      })
    ],

  providers: [
      DataService,
      customerService
    ],

    bootstrap: [AppComponent, Accounting, Login] // 
    
})

export class AppModule { }
