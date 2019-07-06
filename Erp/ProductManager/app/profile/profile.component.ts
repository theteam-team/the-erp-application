import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import * as uuid from 'uuid';

import { DataService } from 'ProductManager/app/shared/dataService';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

    public customerID;
    public orderID;
    public addressID = uuid.v4();

    public address = {
        "id": "",
        "city": "",
        "governate": "",
        "street": "",
        "zip_code": 0,
        "customer_id": "",
        "Crm_employee_id": "string"
    };

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute, public dialogRef: MatDialogRef<ProfileComponent>) {
        this.route.paramMap.subscribe(params => this.customerID = params.get('cid'));
        this.route.paramMap.subscribe(params => this.orderID = params.get('oid'));
    }

    ngOnInit() {
    }

    addCustomerAddress(aForm: NgForm): void {

        this.address.id = this.addressID;
        this.address.city = aForm.value.city;
        this.address.governate = aForm.value.governate;
        this.address.street = aForm.value.street;
        this.address.zip_code = aForm.value.zip_code;
        this.address.customer_id = this.customerID;

        this.data.addCustomerAddress(this.address);
        //this.router.navigate(["confirm", this.customerID, this.orderID]);
        this.onClose();
    }

    onClose(): void {
        this.dialogRef.close();
    }
}
