import { Component, OnInit } from '@angular/core';
import { DataService } from 'ClientApp/app/shared/dataService';
import { Module, Products } from "ClientApp/app/shared/module";

@Component({
    selector: 'accounting-activity',
    templateUrl: 'activity.component.html',
    styleUrls: [],
})

export class AccountingActivity{
    title = 'Accounting';
    constructor(private data: DataService) {
    }

    public sold_products: Products[] = [];

    ngOnInit(): void {
        this.data.loadSoldProducts()
            .subscribe(success => {
                if (success) {
                    this.sold_products = this.data.sold_products;
                }
            });
    }
}