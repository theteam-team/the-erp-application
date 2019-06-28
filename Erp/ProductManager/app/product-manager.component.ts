import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { DataService } from 'ProductManager/app/shared/dataService';


@Component({
  selector: 'product-manager',
  templateUrl: './product-manager.component.html',
  styleUrls: ['./product-manager.component.css']
})
export class ProductManagerComponent implements OnInit {
    title = 'ProductManager';

    public availableProducts = [];

    constructor(private data: DataService, private router: Router, private location: Location) {

    }

    ngOnInit(): void {
        this.loadAvailableProducts();
    }

    reloadComponent(): void {
        location.reload();
    }

    loadAvailableProducts(): void {

        this.data.loadAvailableProducts()
            .subscribe(success => {
                if (success) {
                    this.availableProducts = this.data.availableProducts;
                }
            })
    }
}
