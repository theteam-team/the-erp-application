import { Component, OnInit } from '@angular/core';
import { DataService } from 'ClientApp/app/shared/dataService';
import { Invoice } from "ClientApp/app/shared/module";
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';

@Component({
    selector: 'accounting-activity',
    templateUrl: 'activity.component.html',
    styleUrls: [],
})

export class AccountingActivity implements OnInit{
    title = 'Accounting';
    constructor(private data: DataService, private router: Router) {
    }

    //trial
    public sold_product = [{
        id: "1",
        name: "hi"
    }];
    ngOnInit(): void {
        //this.getSoldProduct();
        this.getInvoice();
    }
    

    public invoice: Invoice[] = [];
    getInvoice(): void {
        this.data.loadInvoice()
            .subscribe(success => {
                if (success) {
                    this.invoice = this.data.invoice;
                }
            });
    }
}