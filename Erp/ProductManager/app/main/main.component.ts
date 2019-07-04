import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import * as uuid from 'uuid';

import { DataService } from 'ProductManager/app/shared/dataService';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

    public today = new Date();
    public dd = String(this.today.getDate()).padStart(2, '0');
    public mm = String(this.today.getMonth() + 1).padStart(2, '0'); //January is 0!
    public yyyy = this.today.getFullYear();

    public date = this.yyyy + "-" + this.mm + "-" + this.dd;
    public customerID = "11";
    public availableProducts = [];
    public orderID = uuid.v4();

    public productInOrder = {
        "orderID": this.orderID,
        "productID": "",
        "unitsOrdered": 0,
        "unitsDone": 0
    };

    public order = {
        "id": this.orderID,
        "incoming": 1,
        "outgoing": 0,
        "requiredDate": this.date,
        "completedDate": "",
        "orderStatus": "Waiting",
        "totalPrice": 0,
        "customerID": this.customerID,
        "supplierID": "",
        "paymentID": "",
        "shipmentID": ""
    };

    constructor(private data: DataService, private router: Router, private location: Location) {
    }

    ngOnInit(): void {
        this.loadAvailableProducts();
        this.addOrder();
    }

    reloadComponent(): void {
        location.reload();
    }

    loadAvailableProducts(): void {

        this.data.loadAvailableProducts()
            .subscribe(success => {
                if (success) {
                    this.availableProducts = this.data.availableProducts;
                }
            })
    }

    addOrder(): void {
        this.data.addOrder(this.order);
    }

    onProductAdd(productID: string, units: number, newPrice: number): void {

        this.productInOrder.productID = productID;
        this.productInOrder.unitsOrdered = units;

        this.data.addToOrder(this.productInOrder);

        this.order.totalPrice = units * newPrice;
        this.data.addToOrderTotal(this.order);
    }

    goToCart() {
        this.router.navigate(["cart", this.customerID, this.orderID]);
    }
}
