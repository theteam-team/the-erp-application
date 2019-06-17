import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-inventories',
    templateUrl: './inventories.component.html',
    styleUrls: ['./inventories.component.css']
})
export class InventoriesComponent implements OnInit {

    constructor(private data: DataService, private router: Router, private location: Location) {
    }

    public inventories = [];

    ngOnInit(): void {
        this.displayAllInventories();
    }

    reloadComponent(): void {
        location.reload();
    }

    onInventoryClick(id: string) {
        this.router.navigate(["warehouse/inventory", id]);
    }

    displayAllInventories(): void {
        this.data.showInventories()
            .subscribe(success => {
                if (success) {
                    this.inventories = this.data.inventories;
                }
            })
    }

    searchInventories(sForm: NgForm): void {
        console.log(sForm.value);
        this.data.searchInventories(sForm.value.sKey, sForm.value.sValue)
            .subscribe(success => {
                if (success) {
                    this.inventories = this.data.inventories;
                }
            })
    }

    addInventory() {
        this.router.navigate(["warehouse/addInventory"]);
    }

    deleteInventory(id: string) {
        this.data.deleteInventory(id);
        this.reloadComponent();
    }
}
