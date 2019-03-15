import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { ModuleList } from './services/moduleList.component';
import { Cart } from './services/cart.component';
import { DataService } from './shared/dataService';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
      AppComponent,
      ModuleList,
      Cart
  ],
  imports: [
      BrowserModule,
      HttpClientModule
  ],
    providers: [
        DataService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
 