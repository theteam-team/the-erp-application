import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {

    public check: boolean;

    constructor(private data: DataService, private router: Router, private location: Location) {
    }

    ngOnInit() {
    }

    reloadComponent(): void {
        location.reload();
    }

    saveProduct(pForm: NgForm): void {
        this.data.saveProduct(pForm.value);
        this.reloadComponent();
    }

    goToProducts() {
        this.check = window.confirm("Are you sure you want to exit without saving?");
        if (this.check) {
            this.router.navigate(["warehouse/products"]);
        }
    }


}
