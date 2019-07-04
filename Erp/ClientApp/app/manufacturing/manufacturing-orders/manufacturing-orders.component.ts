import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-manufacturing-orders',
  templateUrl: './manufacturing-orders.component.html',
  styles: []
})
export class ManufacturingOrdersComponent implements OnInit {


  constructor(private router: Router) { }

  ngOnInit() {
  }

  createOrder() {
    this.router.navigate(["manufacturing/manufacturingOrders/new"]);
  }

}
