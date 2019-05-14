import { Component, OnInit } from '@angular/core';
import { DataService } from 'ClientApp/app/shared/dataService';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.css']
})

export class AddOrderComponent implements OnInit {

    public check: boolean;
    public orderInfo = {
        "id": "",
        "requiredDate": "",
        "completedDate": '2019-12-31',
        "orderStatus": "In Progress",
        "customerID": "",
        "paymentID": ""
    };

    constructor(private data: DataService, private router: Router) { }

    ngOnInit() {
    }

    saveOrder(oForm: NgForm): void {
        this.orderInfo.id = oForm.value.id;
        this.orderInfo.requiredDate = oForm.value.requiredDate;
        this.orderInfo.customerID = oForm.value.customerID;
        this.orderInfo.paymentID = oForm.value.paymentID;

        this.data.saveOrder(this.orderInfo);
        this.router.navigate(["warehouse/addOrder/addProducts", this.orderInfo.id]);
    }

    goToOrders() {
        this.check = window.confirm("Are you sure you want to exit without saving?");
        if (this.check) {
            this.router.navigate(["warehouse/orders"]);
        }
    }
}
