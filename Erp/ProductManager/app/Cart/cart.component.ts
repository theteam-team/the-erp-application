import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material'
import * as uuid from 'uuid';

import { DataService } from 'ProductManager/app/shared/dataService';
import { ProfileComponent } from '../profile/profile.component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

    public customerID;
    public orderID;
    public customerProducts = [];
    public orderInfo = {
        "id": "",
        "incoming": 1,
        "outgoing": 0,
        "requiredDate": "",
        "completedDate": "",
        "orderStatus": "Waiting",
        "totalPrice": 0,
        "customerID": "",
        "supplierID": "",
        "paymentID": "",
        "shipmentID": ""
    };

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute, private location: Location, private dialog: MatDialog) {
        this.route.paramMap.subscribe(params => this.customerID = params.get('cid'));
        this.route.paramMap.subscribe(params => this.orderID = params.get('oid'));
    }

    ngOnInit() {
        this.data.loadCustomerProducts(this.customerID)
            .subscribe(success => {
                if (success) {
                    this.customerProducts = this.data.customerProducts;
                }
            });

        this.data.loadOrderInfo(this.orderID)
            .subscribe(success => {
                if (success) {
                    this.orderInfo = this.data.orderInfo;
                }
            });
    }

    reloadComponent(): void {
        location.reload();
    }

    onProductRemove(oid: string, pid: string, units: number, newPrice: number): void {

        this.data.deleteProductFromOrder(oid, pid);

        this.orderInfo.totalPrice = units * newPrice;
        this.data.removeFromOrderTotal(this.orderInfo);
        this.reloadComponent();
    }

    submitOrder(): void {
        this.router.navigate(["profile", this.customerID, this.orderID]);
        /*const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.width = "60%";

        this.dialog.open(ProfileComponent, dialogConfig);*/
    }

    goToCart() {

    }
}
