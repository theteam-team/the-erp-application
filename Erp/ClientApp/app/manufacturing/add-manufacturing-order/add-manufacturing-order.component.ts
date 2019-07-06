import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { createOrder } from '../modules/module'
@Component({
  selector: 'app-add-manufacturing-order',
  templateUrl: './add-manufacturing-order.component.html',
  styles: []
})
export class AddManufacturingOrderComponent implements OnInit {
  order: createOrder[];
  constructor(private router: Router) { }

  ngOnInit() {
  }

  orderProduct() {
    this.router.navigate(["manufacturing/manufacturingOrders"]);
  }

}
