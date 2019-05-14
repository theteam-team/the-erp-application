import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})

export class OrdersComponent implements OnInit {

    constructor(private data: DataService, private router: Router, private location: Location) {
    }

    public orders = [];

    ngOnInit(): void {
        this.displayAllOrders();
    }

    reloadComponent(): void {
        location.reload();
    }

    onOrderClick(orderid: string) {
        this.router.navigate(["warehouse/order", orderid]);
    }

    displayAllOrders(): void {
        this.data.loadAllOrders()
            .subscribe(success => {
                if (success) {
                    this.orders = this.data.orders;
                }
            })
    }

    displayCompletedOrders(): void{
        this.data.loadCompletedOrders()
            .subscribe(success => {
                if (success) {
                    this.orders = this.data.orders;
                }
            })
    }

    displayOrdersInProgress(): void {
        this.data.loadOrdersInProgress()
            .subscribe(success => {
                if (success) {
                    this.orders = this.data.orders;
                }
            })
    }

    displayReadyOrders(): void {
        this.data.loadReadyOrders()
            .subscribe(success => {
                if (success) {
                    this.orders = this.data.orders;
                }
            })
    }

    searchOrders(sForm: NgForm): void {
        console.log(sForm.value);
        this.data.searchOrders(sForm.value.sKey, sForm.value.sValue)
            .subscribe(success => {
                if (success) {
                    this.orders = this.data.orders;
                }
            })
    }

    addOrder() {
        this.router.navigate(["warehouse/addOrder"]);
    }

    deleteOrder(orderID: string) {
        this.data.deleteOrder(orderID);
        this.reloadComponent();
    }
}
