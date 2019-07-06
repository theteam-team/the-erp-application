import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { createOrder } from '../modules/module'
import { ManufacturingOrderService } from '../services/manufacturing-order.service';
import { NgForm } from '@angular/forms';
@Component({
  selector: 'app-add-manufacturing-order',
  templateUrl: './add-manufacturing-order.component.html',
  styles: []
})
export class AddManufacturingOrderComponent implements OnInit {
  // order: createOrder;

  constructor(private manufacturingOrderService: ManufacturingOrderService, private router: Router) { }

  ngOnInit() {
  }

  orderProduct(order: NgForm) {
    this.manufacturingOrderService.saveProduct(order.value);
    //this.router.navigate(["manufacturing/manufacturingOrders"]);
    console.log(order.value);
  }
}
