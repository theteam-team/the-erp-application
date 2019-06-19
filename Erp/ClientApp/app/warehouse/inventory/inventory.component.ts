import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {

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
    public productsInInventory = [];

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute, private location: Location) {
        this.route.paramMap.subscribe(params => this.inventoryID = params.get('inventoryid'));
    }

    ngOnInit() {
        this.data.showProductsInInventory(this.inventoryID)
            .subscribe(success => {
                if (success) {
                    this.productsInInventory = this.data.productsInInventory;
                }
            })
    }

    reloadComponent(): void {
        location.reload();
    }

    deleteProduct(productID: string) {
        this.data.deleteProductFromInventory(this.inventoryID, productID);
        this.reloadComponent();
    }

    onSave(productID: string, units: number) {
        this.productInInventory.inventoryID = this.inventoryID;
        this.productInInventory.productID = productID;
        this.productInInventory.unitsInInventory = units;

        this.data.editProductInInventory(this.productInInventory);
    }
}
