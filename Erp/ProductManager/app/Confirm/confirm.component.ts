import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import * as uuid from 'uuid';

import { DataService } from 'ProductManager/app/shared/dataService';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent implements OnInit {

    public customerID;
    public orderID;
    public paymentID = uuid.v4();
    public total;

    public payment = {
        "id": this.paymentID,
        "method": "On Delivery",
        "date": "",
        "amount": 0
    };

    public order = {
        "id": "",
        "incoming": 1,
        "outgoing": 0,
        "requiredDate": "",
        "completedDate": "",
        "orderStatus": "Waiting",
        "totalPrice": 0,
        "customerID": "",
        "supplierID": "",
        "paymentID": this.paymentID,
        "shipmentID": ""
    };


    constructor(private data: DataService, private router: Router, private route: ActivatedRoute) {
        this.route.paramMap.subscribe(params => this.customerID = params.get('cid'));
        this.route.paramMap.subscribe(params => this.orderID = params.get('oid'));
    }

    ngOnInit() {
        this.total = this.data.orderInfo.totalPrice;
    }

    onConfirm(): void {
        this.payment.amount = this.total;
        this.order.id = this.orderID;
        this.data.addPayment(this.payment, this.order);
        this.router.navigate(["home"]);
    }

    goToCart() {

    }
}
