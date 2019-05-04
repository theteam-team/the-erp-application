import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DataService } from 'ClientApp/app/shared/dataService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {

    public check: boolean;

    constructor(private data: DataService, private router: Router) { }

    ngOnInit() {
    }

    saveProduct(pForm: NgForm): void {
        this.data.saveProduct(pForm.value);
        this.router.navigate(["warehouse/products"]);
    }

    goToProducts() {
        this.check = window.confirm("Are you sure you want to exit without saving?");
        if (this.check) {
            this.router.navigate(["warehouse/products"]);
        }
    }
}
