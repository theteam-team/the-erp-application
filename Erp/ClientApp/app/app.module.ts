import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
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


let routes = [
    { path: "", component: ModuleList }, 

    { path: "accounting", component: AccountingModule },
    { path: "login", component: Login },

    { path: "warehouse", component: WarehouseComponent },
    { path: "warehouse/products", component: ProductsComponent },
    { path: "warehouse/orders", component: OrdersComponent },
    { path: "warehouse/configuration", component: ConfigurationComponent },
    { path: "warehouse/products/:productid", component: ProductComponent },
    { path: "warehouse/orders/:orderid", component: OrderComponent }
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
      OrderComponent
    ],
  imports: [
      BrowserModule,
      HttpClientModule,
      RouterModule.forRoot(routes, {
          useHash: true,
          enableTracing: false
      })
    ],

  providers: [
       DataService
    ],

  bootstrap: [AppComponent, Accounting] // 
    
})
export class AppModule { }
 