import { Component, OnInit } from '@angular/core';
import { DataService } from 'ClientApp/app/shared/dataService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-products-in-order',
  templateUrl: './add-products-in-order.component.html',
  styleUrls: ['./add-products-in-order.component.css']
})
export class AddProductsInOrderComponent implements OnInit {

    public orderInfo = {
        "orderID": "3",
        "productID": "3",
        "unitsOrdered": 3,
        "unitsDone": 0
    };

    constructor(private data: DataService, private router: Router) { }

    ngOnInit() {
    }

    saveProductsInOrder(): void {
        this.data.saveProductsInOrder(this.orderInfo);
    }
}
