import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.css']
})
export class EditOrderComponent implements OnInit {

    public orderID: string;
    public productsInOrder = [];
    public check: boolean;
    public product = {
        "orderID": "",
        "productID": "",
        "unitsOrderd": 0,
        "unitsDone": 0
    };

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute, private location: Location) {
        this.route.paramMap.subscribe(params => this.orderID = params.get('orderid'));
    }

    ngOnInit() {
        this.data.loadProductsInOrder(this.orderID)
            .subscribe(success => {
                if (success) {
                    this.productsInOrder = this.data.productsInOrder;
                }
            })
    }

    reloadComponent(): void {
        location.reload();
    }

    saveProductEdits(pForm: NgForm): void {
        this.product.orderID = this.orderID;
        this.product.productID = pForm.value.productID;
        this.product.unitsOrderd = pForm.value.unitsOrderd;
        this.product.unitsDone = pForm.value.unitsDone;

        this.data.saveProductEdits(this.product);
    }

    removeFromOrder(pID: string): void {
        this.data.deleteProductFromOrder(this.orderID, pID);
    }

    submitEdits(): void {
        this.reloadComponent();
    }

    goToOrders() {
        if (this.check) {
            this.router.navigate(["warehouse/orders"]);
        }
        this.router.navigate(["warehouse/orders"]);
    }
}
