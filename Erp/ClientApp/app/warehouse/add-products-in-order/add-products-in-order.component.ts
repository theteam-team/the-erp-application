import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-add-products-in-order',
  templateUrl: './add-products-in-order.component.html',
  styleUrls: ['./add-products-in-order.component.css']
})
export class AddProductsInOrderComponent implements OnInit {

    public products = [];
    public productInOrder = {
        "orderID": "",
        "productID": "",
        "unitsOrdered": 0,
        "unitsDone": 0
    };
    public orderID: string;
    
    constructor(private data: DataService, private router: Router, private route: ActivatedRoute) {
        this.route.paramMap.subscribe(params => this.orderID = params.get('orderid'));
    }

    ngOnInit(): void {
        this.displayAllProducts();
    }

    displayAllProducts(): void {
        this.data.loadProducts()
            .subscribe(success => {
                if (success) {
                    this.products = this.data.products;
                }
            })
    }

    searchProducts(sForm: NgForm): void {
        console.log(sForm.value);
        this.data.searchProducts(sForm.value.sKey, sForm.value.sValue)
            .subscribe(success => {
                if (success) {
                    this.products = this.data.products;
                }
            })
    }

    onProductAdd(productID: string, units: number) {
        this.productInOrder.orderID = this.orderID;
        this.productInOrder.productID = productID;
        this.productInOrder.unitsOrdered = units;
        
        this.data.addProductToOrder(this.productInOrder);
    }

    saveProductsInOrder(): void {
        this.router.navigate(["warehouse/addOrder/submitOrder/" + this.orderID]);
    }
}
