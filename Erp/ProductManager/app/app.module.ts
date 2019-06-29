import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { ProductManagerComponent } from './product-manager.component';
import { DataService } from './shared/dataService';
import { SignUpComponent } from './sign-up/sign-up.component';
import { LogInComponent } from './log-in/log-in.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { OrdersComponent } from './orders/orders.component';
import { MainComponent } from './main/main.component';


let routes = [
    { path: "", component: MainComponent },
    { path: "signUp", component: SignUpComponent },
    { path: "logIn", component: LogInComponent },
    { path: "home/:id", component: HomeComponent },
    { path: "profile/:id", component: ProfileComponent },
    { path: "orders/:id", component: OrdersComponent }
];

@NgModule({
    declarations: [
        ProductManagerComponent,
        MainComponent,
        SignUpComponent,
        LogInComponent,
        HomeComponent,
        ProfileComponent,
        OrdersComponent
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
