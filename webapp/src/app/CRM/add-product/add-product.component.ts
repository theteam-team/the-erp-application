import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { Product } from '../models/productModel';
@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styles: []
})
export class AddProductComponent implements OnInit {
  products: Product[];
  containers = [];
  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.getProducts().subscribe(products => this.products = products);
  }

  add() {
    this.containers.push(this.containers.length);
  }
}
