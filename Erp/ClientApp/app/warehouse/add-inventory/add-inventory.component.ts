import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-add-inventory',
  templateUrl: './add-inventory.component.html',
  styleUrls: ['./add-inventory.component.css']
})
export class AddInventoryComponent implements OnInit {

    public check: boolean;

    constructor(private data: DataService, private router: Router, private location: Location) {
    }

    ngOnInit() {
    }

    reloadComponent(): void {
        location.reload();
    }

    saveInventory(iForm: NgForm): void {
        this.data.saveInventory(iForm.value);
        this.reloadComponent();
    }

    goToInventories() {
        this.check = window.confirm("Are you sure you want to exit without saving?");
        if (this.check) {
            this.router.navigate(["warehouse/inventories"]);
        }
    }

}
