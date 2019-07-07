import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Products } from '../modules/module'
import { BillofMatrials } from '../modules/module';
import { ManufacturingOrderService } from '../services/manufacturing-order.service';
import { ProductsService } from '../services/products.service';
import { MaterialService } from '../services/material.service';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-add-manufacturing-order',
  templateUrl: './add-manufacturing-order.component.html',
  styles: []
})
export class AddManufacturingOrderComponent implements OnInit {
  // order: createOrder;
  products: Products[];
  materials: BillofMatrials[];
  constructor(private manufacturingOrderService: ManufacturingOrderService, private productService: ProductsService, private materialService: MaterialService, private router: Router) { }

  ngOnInit() {
    this.productService.getProducts().subscribe(products => this.products = products);
    this.materialService.getMaterials().subscribe(materials => this.materials = materials);
  }

  orderProduct(order: NgForm) {
    this.manufacturingOrderService.saveProduct(order.value);
    this.router.navigate(["manufacturing"]);
    //console.log(order.value);
  }
}
