import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})

export class OrderComponent implements OnInit {

    public orderID: string;
    public orderInfo;
    public formDisabled: boolean;
    public check: boolean;
    public orderStatus: string;

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute, private location: Location) {
        this.route.paramMap.subscribe(params => this.orderID = params.get('orderid'));
    }

    ngOnInit() {
        this.formDisabled = true;

        this.data.loadOrderInfo(this.orderID)
            .subscribe(success => {
                if (success) {
                    this.orderInfo = this.data.orderInfo;
                }
            });
    }

    saveOrderEdits(oForm: NgForm): void {

        this.orderInfo.id = this.orderID;
        if (oForm.value.requiredDate) {
            this.orderInfo.requiredDate = oForm.value.requiredDate;
        }
        if (oForm.value.customerID) {
            this.orderInfo.customerID = oForm.value.customerID;
        }
        if (oForm.value.paymentID) {
            this.orderInfo.paymentID = oForm.value.paymentID;
        }
        if (oForm.value.orderStatus) {
            this.orderInfo.orderStatus = oForm.value.orderStatus;
        }

        this.data.saveOrderEdits(this.orderInfo);
        this.router.navigate(["warehouse/order/editOrder", this.orderID]);
    }

    editOrder() {
        this.formDisabled = false;
    }

    goToOrders() {
        if (!this.formDisabled) {
            this.check = window.confirm("Are you sure you want to exit without saving?");
            if (this.check) {
                this.router.navigate(["warehouse/orders"]);
            }
        }
        this.router.navigate(["warehouse/orders"]);
    }
}
