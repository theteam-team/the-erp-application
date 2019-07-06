import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ManufacturingOrderService } from '../services/manufacturing-order.service';

import { manufacturingOrders } from '../modules/module'
@Component({
  selector: 'app-manufacturing-orders',
  templateUrl: './manufacturing-orders.component.html',
  styles: []
})
export class ManufacturingOrdersComponent implements OnInit {

  orders: manufacturingOrders[];

  constructor(private manufacturingOrderService: ManufacturingOrderService, private router: Router) { }

  ngOnInit() {
    this.manufacturingOrderService.getOrders().subscribe(orders => this.orders = orders);
  }

  createOrder() {
    this.router.navigate(["manufacturing/manufacturingOrders/new"]);
  }

}
