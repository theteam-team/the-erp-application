import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import * as uuid from 'uuid';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {

    public check: boolean;
    public product = {
        "id": uuid.v4(),
        "name": "",
        "description": "",
        "price": 0,
        "weight": 0,
        "length": 0,
        "width": 0,
        "height": 0,
        "unitsInStock": 0,
        "sold": 1,
        "purchased": 0
    };

    constructor(private data: DataService, private router: Router, private location: Location) {
    }

    ngOnInit() {
    }

    reloadComponent(): void {
        location.reload();
    }

    saveProduct(pForm: NgForm): void {

        this.product.name = pForm.value.name;
        if (pForm.value.description)
            this.product.description = pForm.value.description;
        this.product.price = pForm.value.price;
        this.product.weight = pForm.value.weight;
        this.product.length = pForm.value.length;
        this.product.width = pForm.value.width;
        this.product.height = pForm.value.height;
        this.product.unitsInStock = pForm.value.unitsInStock;

        this.data.saveProduct(this.product);
        this.reloadComponent();
    }

    goToProducts() {
        this.check = window.confirm("Are you sure you want to exit without saving?");
        if (this.check) {
            this.router.navigate(["warehouse/products"]);
        }
    }


}
