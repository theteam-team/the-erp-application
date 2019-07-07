import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
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
    public addressID = uuid.v4();
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

    public address = {
        "id": "",
        "city": "",
        "governate": "",
        "street": "",
        "zip_code": 0,
        "customer_id": "",
        "Crm_employee_id": "string"
    };

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute) {
        this.route.paramMap.subscribe(params => this.customerID = params.get('cid'));
        this.route.paramMap.subscribe(params => this.orderID = params.get('oid'));
    }

    ngOnInit() {
        this.total = this.data.orderInfo.totalPrice;
    }

    addCustomerAddress(aForm: NgForm): void {

        this.address.id = this.addressID;
        this.address.city = aForm.value.city;
        this.address.governate = aForm.value.governate;
        this.address.street = aForm.value.street;
        this.address.zip_code = aForm.value.zip_code;
        this.address.customer_id = this.customerID;

        this.data.addCustomerAddress(this.address);
    }

    onConfirm(): void {
        this.payment.amount = this.total;
        this.order.id = this.orderID;
        this.data.addPayment(this.payment, this.order);
    }

    goToCart() {
        this.router.navigate(["cart", this.customerID, this.orderID]);
    }
}
