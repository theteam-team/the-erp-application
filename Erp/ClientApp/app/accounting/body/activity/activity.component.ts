import { Component, OnInit } from '@angular/core';
import { DataService } from 'ClientApp/app/shared/dataService';
import { Products } from "ClientApp/app/shared/module";

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