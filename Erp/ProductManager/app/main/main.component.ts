import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

import { DataService } from 'ProductManager/app/shared/dataService';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

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
