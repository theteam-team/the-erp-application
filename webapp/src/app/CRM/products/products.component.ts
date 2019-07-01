import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material'
import { ProductService } from '../services/product.service';
import { Product } from '../models/productModel';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styles: []
})
export class ProductsComponent implements OnInit {

  products: Product[];

  constructor(public dialogRef: MatDialogRef<ProductsComponent>, private productService: ProductService) { }

  ngOnInit() {
    this.productService.getProducts().subscribe(products => this.products = products);
  }

  onSubmit() {
    this.onClose();
  }
  onClose() {
    this.dialogRef.close;
  }
  editProduct(id:number) {
     
  }
  
}
