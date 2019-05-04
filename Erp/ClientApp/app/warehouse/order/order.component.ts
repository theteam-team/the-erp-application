import { Component, OnInit } from '@angular/core';
import { DataService } from 'ClientApp/app/shared/dataService';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})

export class OrderComponent implements OnInit {

    public orderID: string;

    constructor(private data: DataService, private route: ActivatedRoute) {
        this.route.paramMap.subscribe(params => this.orderID = params.get('orderid'));
    }

    public orderInfo;

    ngOnInit() {
        this.data.loadOrderInfo(this.orderID)
            .subscribe(success => {
                if (success) {
                    this.orderInfo = this.data.orderInfo;
                }
            })
    }

}
