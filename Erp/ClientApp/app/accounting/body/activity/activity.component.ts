import { Component, OnInit } from '@angular/core';
import { DataService } from 'ClientApp/app/shared/dataService';
import { Products, Invoice } from "ClientApp/app/shared/module";

@Component({
    selector: 'accounting-activity',
    templateUrl: 'activity.component.html',
    styleUrls: [],
})

export class AccountingActivity implements OnInit{
    title = 'Accounting';
    constructor(private data: DataService) {
    }

    //trial
    public sold_product = [{
        id: "1",
        name: "hi"
    }];
    ngOnInit(): void {
        this.getSoldProduct();
        this.getInvoice();
    }
    public sold_products: Products[] = [];
    getSoldProduct(): void{
        this.data.loadSoldProducts()
            .subscribe(success => {
                if (success) {
                    this.sold_products = this.data.sold_products;
                }
            });
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