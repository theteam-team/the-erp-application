import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-receipts',
  templateUrl: './receipts.component.html',
  styleUrls: ['./receipts.component.css']
})

export class ReceiptsComponent implements OnInit {

    constructor(private data: DataService, private router: Router, private location: Location) {
    }

    public orders = [];

    ngOnInit(): void {
        this.displayReceipts();
    }

    reloadComponent(): void {
        location.reload();
    }

    onOrderClick(orderid: string) {
        this.router.navigate(["warehouse/order", orderid]);
    }

    displayReceipts(): void {
        this.data.loadReceipts()
            .subscribe(success => {
                if (success) {
                    this.orders = this.data.orders;
                }
            })
    }

    displayCompletedOrders(): void{
        this.data.loadCompletedReceipts()
            .subscribe(success => {
                if (success) {
                    this.orders = this.data.orders;
                }
            })
    }


    displayWaitingOrders(): void {
        this.data.loadWaitingReceipts()
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
