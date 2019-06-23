import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
    selector: 'app-product-moves',
    templateUrl: './product-moves.component.html',
    styleUrls: ['./product-moves.component.css']
})
export class ProductMovesComponent implements OnInit {

    public productsMoves = [];

    constructor(private data: DataService, private router: Router) {

    }

    ngOnInit(): void {

        this.data.getProductsMoves()
            .subscribe(success => {
                if (success) {
                    this.productsMoves = this.data.productsMoves;
                }
            })
    }

}
