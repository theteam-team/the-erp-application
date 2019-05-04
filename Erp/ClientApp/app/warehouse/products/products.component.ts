import { Component, OnInit } from '@angular/core';
import { DataService } from 'ClientApp/app/shared/dataService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {

    constructor(private data: DataService, private router: Router) {
    }

    public products = [];

    ngOnInit(): void {
        this.displayAllProducts();
    }

    displayAllProducts(): void {
        this.data.loadProducts()
            .subscribe(success => {
                if (success) {
                    this.products = this.data.products;
                }
            })
    }

    searchProducts(key: string, value: string): void {
        this.data.searchProducts(key, value)
            .subscribe(success => {
                if (success) {
                    this.products = this.data.products;
                }
            })
    }
    
    onProductClick(productid: string) {
        this.router.navigate(["warehouse/product", productid]);
    }

    addProduct() {
        this.router.navigate(["warehouse/addproduct"]);
    }

    deleteProduct(productid: string) {
        this.data.deleteProduct(productid);
        this.router.navigate(["warehouse/products"]);
    }
}
