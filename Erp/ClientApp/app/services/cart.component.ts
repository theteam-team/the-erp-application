import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/dataService';

@Component({
    selector: 'the-cart',
    templateUrl: 'cart.component.html',
    styleUrls: ['cart.component.css'],
})

export class Cart {
    constructor(private data: DataService) {
    }

    openNav() {
        document.getElementById("mySidenav").style.width = "250px";
    }

    closeNav() {
        document.getElementById("mySidenav").style.width = "0";
    }
}