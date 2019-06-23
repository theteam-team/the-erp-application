import { Component, OnInit } from '@angular/core';
import { DataService } from 'ClientApp/app/shared/dataService';
import { Customer, customerOrders, OrderProducts } from "ClientApp/app/shared/module";
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'accounting-summary',
    templateUrl: 'summary.component.html',
    styleUrls: ['summary.component.css']

})

export class AccountingSummary implements OnInit {
    title = 'Accounting';
    constructor(private data: DataService, private router: Router) {
    }
    ngOnInit(): void {
     //   this.loadCustomerByID("2");
    }

    public customer: Customer[]  =[];
    loadCustomerByID(newHero: string): void {
        console.log(newHero);
        this.data.loadCustomerByID(newHero)
            .subscribe(success => {
                if (success) {
                    this.customer = this.data.customer;
                }
            })
    }

    public customer_orders: customerOrders[] = [];
    loadCustomerOrders(customer_id: string): void {
        console.log(customer_id);
        this.data.loadCustomerOrders(customer_id)
            .subscribe(success => {
                if (success) {
                    this.customer_orders = this.data.customer_orders;
                }
            })
    }

    public order_products: OrderProducts[] = [];
    loadOrderProducts(id: string): void {
        console.log(id);
        this.data.loadOrderProducts(id)
            .subscribe(success => {
                if (success) {
                    this.order_products = this.data.order_products;
                }
            })
    }

    customers = [
        {
            customer_id: "5",
            name: "Ali",
            phone_number: 0          
        
        }];
    customerss = "1";

    totalCounts(data) {
        let total = 0;

        data.forEach((d) => {
            total += parseInt(d.count, 10);
        });

        return total;
    }

}
