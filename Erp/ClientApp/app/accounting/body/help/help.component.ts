import { Component, OnInit } from '@angular/core';
import { Products } from "ClientApp/app/shared/module";
import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
    selector: 'accounting-help',
    templateUrl: 'help.component.html'
})

export class AccountingHelp implements OnInit {
    title = 'Accounting';
    constructor(private data: DataService) {
    }
    ngOnInit(): void {
        this.getSoldProduct();
        //this.getInvoice();
    }
    public sold_products: Products[] = [];
    getSoldProduct(): void {
        this.data.loadSoldProducts()
            .subscribe(success => {
                if (success) {
                    this.sold_products = this.data.sold_products;
                }
            });
    }
}