import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../services/products.service';
import { Products, BillofMatrials } from '../modules/module';
import { MaterialService } from '../services/material.service';
 

@Component({
  selector: 'app-manufacturing-products',
  templateUrl: './manufacturing-products.component.html',
  styles: []
})
export class ManufacturingProductsComponent implements OnInit {

    products: Products[];
    materials: BillofMatrials[];

    constructor(private productService: ProductsService, private materialService: MaterialService) { }

    ngOnInit() {
        this.productService.getProducts().subscribe(products => this.products = products);
        this.materialService.getMaterials().subscribe(materials => this.materials = materials);

  }

}
