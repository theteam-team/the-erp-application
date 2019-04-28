import { Component, OnInit } from '@angular/core';
import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent implements OnInit {

    constructor(private data: DataService) {
    }

    public products = [];

    ngOnInit(): void {
        //this.products = this.data.products;
        this.data.loadProducts()
            .subscribe(success => {
                if (success) {
                    this.products = this.data.products;
                }
            })
    }

    
}
