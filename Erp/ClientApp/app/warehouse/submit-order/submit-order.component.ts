import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-submit-order',
  templateUrl: './submit-order.component.html',
  styleUrls: ['./submit-order.component.css']
})
export class SubmitOrderComponent implements OnInit {

    public orderID: string;

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute, private location: Location) {
        this.route.paramMap.subscribe(params => this.orderID = params.get('orderid'));
    }

    public productsInOrder = [];

    ngOnInit(): void {
        this.displayAllProducts(this.orderID);
    }

    reloadComponent(): void {
        location.reload();
    }

    displayAllProducts(orderID: string): void {
        this.data.loadProductsInOrder(orderID)
            .subscribe(success => {
                if (success) {
                    this.productsInOrder = this.data.productsInOrder;
                }
            })
    }

    onProductRemove(productID: string): void {
        this.data.deleteProductFromOrder(this.orderID, productID);
        this.reloadComponent();
    }

    submitOrder(): void {
        this.router.navigate(["warehouse/orders"]);
    }

    goBack(): void {
        this.router.navigate(["warehouse/addOrder/addProducts/" + this.orderID]);
    }
}
