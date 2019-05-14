import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {

    public products = [];

    constructor(private data: DataService, private router: Router, private location: Location) {
        
    }

    ngOnInit(): void {
        this.displayAllProducts();
    }

    reloadComponent(): void {
        location.reload();
    }

    displayAllProducts(): void {

        this.data.loadProducts()
            .subscribe(success => {
                if (success) {
                    this.products = this.data.products;
                }
            })
    }

    searchProducts(sForm: NgForm): void {
        console.log(sForm.value);
        this.data.searchProducts(sForm.value.sKey, sForm.value.sValue)
            .subscribe(success => {
                if (success) {
                    this.products = this.data.products;
                }
            })
    }
    
    onProductClick(productID: string) {
        this.router.navigate(["warehouse/product", productID]);
    }

    addProduct() {
        this.router.navigate(["warehouse/addproduct"]);
    }

    deleteProduct(productID: string) {
        this.data.deleteProduct(productID);
        this.reloadComponent();
    }
}
