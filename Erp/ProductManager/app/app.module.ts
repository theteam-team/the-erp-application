import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { ProductManagerComponent } from './product-manager.component';
import { DataService } from './shared/dataService';


let routes = [];

@NgModule({
    declarations: [
        ProductManagerComponent
    ],

    imports: [
        BrowserModule,
        HttpClientModule,

        RouterModule.forRoot(routes, {
            useHash: true,
            enableTracing: false,
            onSameUrlNavigation: "reload"
        })

    ],

    providers: [DataService],

    bootstrap: [ProductManagerComponent]
})

export class AppModule { }
