import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { MatCardModule, MatButtonModule, MatDialogModule, MatInputModule } from '@angular/material';

import { ProductManagerComponent } from './product-manager.component';
import { DataService } from './shared/dataService';
import { ProfileComponent } from './profile/profile.component';
import { MainComponent } from './main/main.component';
import { CartComponent } from './Cart/cart.component';
import { ConfirmComponent } from './Confirm/confirm.component';


let routes = [
    { path: "", component: MainComponent },
    { path: "home", component: MainComponent },
    { path: "cart/:id", component: CartComponent },
    { path: "profile/:id", component: ProfileComponent },
    { path: "confirm/:id", component: ConfirmComponent }
];

@NgModule({
    declarations: [
        ProductManagerComponent,
        MainComponent,
        CartComponent,
        ProfileComponent,
        ConfirmComponent
    ],

    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        MatDialogModule,
        MatInputModule,

        RouterModule.forRoot(routes, {
            useHash: true,
            enableTracing: false,
            onSameUrlNavigation: "reload"
        })

    ],

    providers: [DataService],

    bootstrap: [ProductManagerComponent],

    entryComponents: []
})

export class AppModule { }
