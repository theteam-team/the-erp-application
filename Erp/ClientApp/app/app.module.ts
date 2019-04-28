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


let routes = [
    { path: "", component: ModuleList }, 

    { path: "accounting", component: AccountingModule },
    { path: "login", component: Login },

    { path: "warehouse", component: WarehouseComponent },
    { path: "warehouse/products", component: ProductsComponent }
];
@NgModule({
  declarations: [
      AppComponent,
      ModuleList,
      Accounting,
      AccountingModule,
      Login,
      WarehouseComponent,
      ProductsComponent
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
 