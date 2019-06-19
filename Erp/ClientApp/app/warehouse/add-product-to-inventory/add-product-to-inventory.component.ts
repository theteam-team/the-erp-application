import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-add-product-to-inventory',
  templateUrl: './add-product-to-inventory.component.html',
  styleUrls: ['./add-product-to-inventory.component.css']
})
export class AddProductToInventoryComponent implements OnInit {

    public check: boolean;
    public inventoryID: string;
    public productInInventory = {
        "inventoryID": "",
        "productID": "",
        "name": "",
        "position": "",
        "weight": 0,
        "length": 0,
        "width": 0,
        "height": 0,
        "unitsInInventory": 0,
    };

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute, private location: Location) {
        this.route.paramMap.subscribe(params => this.inventoryID = params.get('inventoryid'));
    }

    ngOnInit() {
    }

    reloadComponent(): void {
        location.reload();
    }

    saveProduct(pForm: NgForm): void {
        this.productInInventory.inventoryID = this.inventoryID;
        this.productInInventory.productID = pForm.value.productID;
        this.productInInventory.position = pForm.value.position;
        this.productInInventory.unitsInInventory = pForm.value.unitsInInventory;

        this.data.saveProduct(this.productInInventory);
        this.reloadComponent();
    }

    goToInventory() {
        this.check = window.confirm("Are you sure you want to exit without saving?");
        if (this.check) {
            this.router.navigate(["warehouse/inventory", this.inventoryID]);
        }
    }

}
