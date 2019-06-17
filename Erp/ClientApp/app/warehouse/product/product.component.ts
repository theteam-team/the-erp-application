import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})

export class ProductComponent implements OnInit {

    public productID: string;
    public formDisabled: boolean;
    public check: boolean;
    public productInfo;

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute, private location: Location) {
        this.route.paramMap.subscribe(params => this.productID = params.get('productid'));
    }

    ngOnInit() {
        this.data.loadProductInfo(this.productID)
            .subscribe(success => {
                if (success) {
                    this.productInfo = this.data.productInfo;
                }
            })
        this.formDisabled = true;
    }

    reloadComponent(): void {
        location.reload();
    }

    saveEdits(pForm: NgForm): void {

        if (pForm.value.name) {
            this.productInfo.name = pForm.value.name;
        }
        if (pForm.value.description) {
            this.productInfo.description = pForm.value.description;
        }
        if (pForm.value.price) {
            this.productInfo.price = pForm.value.price;
        }
        if (pForm.value.weight) {
            this.productInfo.weight = pForm.value.weight;
        }
        if (pForm.value.length) {
            this.productInfo.length = pForm.value.length;
        }
        if (pForm.value.width) {
            this.productInfo.width = pForm.value.width;
        }
        if (pForm.value.height) {
            this.productInfo.height = pForm.value.height;
        }
        if (pForm.value.unitsInStock) {
            this.productInfo.unitsInStock = pForm.value.unitsInStock;
        }

        this.data.saveEdits(this.productInfo);
        this.reloadComponent();
    }

    editProduct() {
        this.formDisabled = false;
    }

    goToProducts() {
        if (!this.formDisabled) {
            this.check = window.confirm("Are you sure you want to exit without saving?");
            if (this.check) {
                this.router.navigate(["warehouse/products"]);
            }
        }
        this.router.navigate(["warehouse/products"]);
    }
}
