import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../services/products.service';
import { Products } from '../modules/module';
 

@Component({
  selector: 'app-manufacturing-products',
  templateUrl: './manufacturing-products.component.html',
  styles: []
})
export class ManufacturingProductsComponent implements OnInit {

    products: Products[];
    constructor(private productService: ProductsService) { }

    ngOnInit() {
        this.productService.getProducts().subscribe(products => this.products = products);
  }

}
