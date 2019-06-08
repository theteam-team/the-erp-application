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
import { AddProductsInOrderComponent } from './warehouse/add-products-in-order/add-products-in-order.component';
import { SubmitOrderComponent } from './warehouse/submit-order/submit-order.component';
import { EditOrderComponent } from './warehouse/edit-order/edit-order.component';
/*
 
import { CrmComponent } from './CRM/crm.component';
import { CreateCustomerComponent } from './CRM/create-customer/create-customer.component';
import { CreateOpportunityComponent } from './CRM/create-opportunity/create-opportunity.component';
import { PipelineComponent } from './CRM/pipline/pipline.component';
import { CustomersComponent } from './CRM/customers/customers.component';
import { CustomerCanDeactivateGuardService } from './CRM/create-customer/customer-canDeacrivare-guard-service';
import { OpportunityCanDeactivateGuardService } from './CRM/create-opportunity/opportunity-canDeactivate-guard-service';
*/

let routes = [

    { path: "", component: ModuleList }, 
    { path: "accounting", component: AccountingModule },

    { path: "warehouse", component: WarehouseComponent },
    { path: "warehouse/products", component: ProductsComponent },
    { path: "warehouse/orders", component: OrdersComponent },
    { path: "warehouse/configuration", component: ConfigurationComponent },
    { path: "warehouse/product/:productid", component: ProductComponent },
    { path: "warehouse/order/:orderid", component: OrderComponent },
    { path: "warehouse/order/editOrder/:orderid", component: EditOrderComponent },
    { path: "warehouse/addproduct", component: AddProductComponent },
    { path: "warehouse/addOrder", component: AddOrderComponent },
    { path: "warehouse/addOrder/addProducts/:orderid", component: AddProductsInOrderComponent },
    { path: "warehouse/addOrder/submitOrder/:orderid", component: SubmitOrderComponent }
    /*
    { path: "crm", component: CrmComponent },

    { path: '', component: PipelineComponent },
    { path: 'customers', component: CustomersComponent },
    {
        path: 'editCustomer/:id',
        component: CreateCustomerComponent,
        canDeactivate: [CustomerCanDeactivateGuardService]
    },
    {
        path: 'editOpportunity/:id',
        component: CreateOpportunityComponent,
        canDeactivate: [OpportunityCanDeactivateGuardService]
    },
    */
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
      
     // CrmComponent,
     
      AddProductComponent,
      AddOrderComponent,
      AddProductsInOrderComponent,
      SubmitOrderComponent,
      EditOrderComponent,
      //CreateCustomerComponent,
      //CreateOpportunityComponent,
      //PipelineComponent,
      //CustomersComponent
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
       
    ],

    bootstrap: [AppComponent, Accounting, Login] // 
    
})

export class AppModule { }
