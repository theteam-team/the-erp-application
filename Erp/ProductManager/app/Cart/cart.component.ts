import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material';
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
    public customerProducts = [];
    public total = 0;

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute, private location: Location, private dialog: MatDialog) {
        this.route.paramMap.subscribe(params => this.customerID = params.get('id'));
    }

    ngOnInit() {

        this.getProducts();
        console.log(this.total);
    }

    reloadComponent(): void {
        location.reload();
    }

    getProducts(): void {
        this.data.loadCustomerProducts(this.customerID)
            .subscribe(success => {
                if (success) {
                    this.customerProducts = this.data.customerProducts;
                }
            });

        this.total = this.customerProducts[this.customerProducts.length - 1].total;
        this.getTotal(this.total);
    }

    getTotal(payment){
        /*let payment = 0;

        for (let p of this.customerProducts) {
            payment += p.price * p.unitsOrdered;
        }*/

        this.data.getTotal(payment);
    }

    onProductRemove(oid, pid): void {

        this.data.deleteProductFromOrder(oid, pid);
        this.reloadComponent();
    }

    submitOrder(): void {
        this.router.navigate(["/profile", this.customerID]);
        /*const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.width = "60%";

        this.dialog.open(ProfileComponent, dialogConfig);*/
    }
}
