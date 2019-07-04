import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { MatCardModule, MatButtonModule } from '@angular/material';
import { AppComponent } from './app.component';
//import { Login } from './login/login.component';
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
import { ReceiptsComponent } from './warehouse/Receipts/receipts.component';
import { InventoriesComponent } from './warehouse/inventories/inventories.component';
import { ProductComponent } from './warehouse/product/product.component';
import { OrderComponent } from './warehouse/order/order.component';
import { ReportingComponent } from './warehouse/reporting/reporting.component';
import { ProductMovesComponent } from './warehouse/product-moves/product-moves.component';
import { AddProductComponent } from './warehouse/add-product/add-product.component';
import { AddOrderComponent } from './warehouse/add-order/add-order.component';
import { AddProductsInOrderComponent } from './warehouse/add-products-in-order/add-products-in-order.component';
import { SubmitOrderComponent } from './warehouse/submit-order/submit-order.component';
import { EditOrderComponent } from './warehouse/edit-order/edit-order.component';
import { InventoryComponent } from './warehouse/inventory/inventory.component';
import { AddInventoryComponent } from './warehouse/add-inventory/add-inventory.component';
import { AddProductToInventoryComponent } from './warehouse/add-product-to-inventory/add-product-to-inventory.component';


import { CrmComponent } from './crm/crm.component';
import { CustomersComponent } from './crm/customers/customers.component';
import { PipelineComponent } from './crm/pipeline/pipeline.component';
import { CreateOpportunityComponent } from './crm/create-opportunity/create-opportunity.component';
import { CreateCustomerComponent } from './crm/create-customer/create-customer.component';
import { customerService } from './crm/customers/customer.service';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CustomerFilterPipe } from './crm/customers/customer-filter';
import { OpportunityFilterPipe } from './crm/pipeline/opportunity-filter';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OpportunityService } from './crm/create-opportunity/opportunity.service';
import { OpportunityCanDeactivateGuardService } from './crm/create-opportunity/opportunity-canDeactivate-guard-service';
import { CustomerCanDeactivateGuardService } from './crm/create-customer/customer-canDeactivate-guard-service';
import { DashboardComponent } from './CRM/dashboard/dashboard.component';


import { ManufacturingComponent } from './manufacturing/manufacturing.component';
import { ManufacturingOrdersComponent } from './manufacturing/manufacturing-orders/manufacturing-orders.component';
import { BillOfMaterialsComponent } from './manufacturing/bill-of-materials/bill-of-materials.component';
import { ManufacturingProductsComponent } from './manufacturing/manufacturing-products/manufacturing-products.component';
import { AddManufacturingOrderComponent } from './manufacturing/add-manufacturing-order/add-manufacturing-order.component';
import { AddManufacturingProductComponent } from './manufacturing/add-manufacturing-product/add-manufacturing-product.component';


let routes = [

  { path: "", component: ModuleList },
  { path: "accounting", component: AccountingModule },
  { path: "accounting/activity", component: AccountingActivity },
  { path: "accounting/summary", component: AccountingSummary },
  { path: "accounting/help", component: AccountingHelp },
  { path: "accounting/offers", component: AccountingOffers },
  { path: "accounting/sendrequest", component: AccountingSendRequest },
  { path: "accounting/wallet", component: AccountingWallet },

  { path: "warehouse", component: ReportingComponent },
  { path: "warehouse/products", component: ProductsComponent },
  { path: "warehouse/operations/deliveries", component: OrdersComponent },
  { path: "warehouse/operations/receipts", component: ReceiptsComponent },
  { path: "warehouse/inventories", component: InventoriesComponent },
  { path: "warehouse/product/:productid", component: ProductComponent },
  { path: "warehouse/order/:orderid", component: OrderComponent },
  { path: "warehouse/inventory/:inventoryid", component: InventoryComponent },
  { path: "warehouse/order/editOrder/:orderid", component: EditOrderComponent },
  { path: "warehouse/products/productMoves", component: ProductMovesComponent },
  { path: "warehouse/addInventory", component: AddInventoryComponent },
  { path: "warehouse/addproduct", component: AddProductComponent },
  { path: "warehouse/addOrder", component: AddOrderComponent },
  { path: "warehouse/inventory/addProduct/:inventoryid", component: AddProductToInventoryComponent },
  { path: "warehouse/addOrder/addProducts/:orderid", component: AddProductsInOrderComponent },
  { path: "warehouse/addOrder/submitOrder/:orderid", component: SubmitOrderComponent },

  { path: "manufacturing", component: ManufacturingComponent },
  { path: "manufacturing/manufacturingOrders", component: ManufacturingOrdersComponent },
  { path: "manufacturing/manufacturingOrders/new", component: AddManufacturingOrderComponent },



  { path: 'crm', component: CrmComponent },
  { path: 'crm/pipeline', component: PipelineComponent },
  { path: 'crm/customers', component: CustomersComponent },
  {
    path: 'crm/editCustomer/:id',
    component: CreateCustomerComponent,
    canDeactivate: [CustomerCanDeactivateGuardService]
  },
  {
    path: 'crm/editOpportunity/:id',
    component: CreateOpportunityComponent,
    canDeactivate: [OpportunityCanDeactivateGuardService]
  },

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

    WarehouseComponent,
    ProductsComponent,
    OrdersComponent,
    ReceiptsComponent,
    InventoriesComponent,
    ProductComponent,
    OrderComponent,
    ReportingComponent,
    ProductMovesComponent,
    InventoryComponent,
    AddInventoryComponent,
    AddProductComponent,
    AddOrderComponent,
    AddProductsInOrderComponent,
    AddProductToInventoryComponent,
    SubmitOrderComponent,
    EditOrderComponent,

    CrmComponent,
    CustomersComponent,
    PipelineComponent,
    CreateOpportunityComponent,
    CreateCustomerComponent,
    CustomerFilterPipe,
    OpportunityFilterPipe,
    DashboardComponent,

    ManufacturingComponent,
    ManufacturingOrdersComponent,
    BillOfMaterialsComponent,
    ManufacturingProductsComponent,
    AddManufacturingOrderComponent,
    AddManufacturingProductComponent,

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,

    HttpClientModule,
    DragDropModule,
    BrowserAnimationsModule,

    RouterModule.forRoot(routes, {
      useHash: true,
      enableTracing: false,
      onSameUrlNavigation: "reload"
    })
  ],


  providers: [
    DataService,
    customerService, OpportunityService, OpportunityCanDeactivateGuardService, CustomerCanDeactivateGuardService

  ],

  bootstrap: [AppComponent, Accounting]//, Login]

})

export class AppModule { }
