import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { manufacturingOrders } from '../modules/module'
@Component({
  selector: 'app-manufacturing-orders',
  templateUrl: './manufacturing-orders.component.html',
  styles: []
})
export class ManufacturingOrdersComponent implements OnInit {

  orders: manufacturingOrders[] = [
    {
      manufacturingOrderid: "1",
      productname: "hp",
      deadlinestart: "25/1/2020",
      quantity: 200,
      rawmaterilasStatus: "waiting",
      orderStatus: "planned",
    },
    {
      manufacturingOrderid: "2",
      productname: "dell",
      deadlinestart: "30/1/2020",
      quantity: 100,
      rawmaterilasStatus: "available",
      orderStatus: "confirmed",
    },
    {
      manufacturingOrderid: "2",
      productname: "dell",
      deadlinestart: "30/1/2020",
      quantity: 100,
      rawmaterilasStatus: "available",
      orderStatus: "confirmed",
    },
    {
      manufacturingOrderid: "2",
      productname: "dell",
      deadlinestart: "30/1/2020",
      quantity: 100,
      rawmaterilasStatus: "available",
      orderStatus: "confirmed",
    },
    {
      manufacturingOrderid: "2",
      productname: "dell",
      deadlinestart: "30/1/2020",
      quantity: 100,
      rawmaterilasStatus: "available",
      orderStatus: "confirmed",
    },
    {
      manufacturingOrderid: "2",
      productname: "dell",
      deadlinestart: "30/1/2020",
      quantity: 100,
      rawmaterilasStatus: "available",
      orderStatus: "confirmed",
    },
    {
      manufacturingOrderid: "2",
      productname: "dell",
      deadlinestart: "30/1/2020",
      quantity: 100,
      rawmaterilasStatus: "available",
      orderStatus: "confirmed",
    },
    {
      manufacturingOrderid: "2",
      productname: "dell",
      deadlinestart: "30/1/2020",
      quantity: 100,
      rawmaterilasStatus: "available",
      orderStatus: "confirmed",
    },
    {
      manufacturingOrderid: "2",
      productname: "dell",
      deadlinestart: "30/1/2020",
      quantity: 100,
      rawmaterilasStatus: "available",
      orderStatus: "confirmed",
    },
  ];

  constructor(private router: Router) { }

  ngOnInit() {
  }

  createOrder() {
    this.router.navigate(["manufacturing/manufacturingOrders/new"]);
  }

}
