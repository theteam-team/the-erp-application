import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-add-manufacturing-order',
  templateUrl: './add-manufacturing-order.component.html',
  styles: []
})
export class AddManufacturingOrderComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  returnToOrders() {
    this.router.navigate(["manufacturing/manufacturingOrders"]);
  }

}
