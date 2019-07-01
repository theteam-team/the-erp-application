import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { DataService } from 'ProductManager/app/shared/dataService';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent implements OnInit {

    public customerID;
    public total;

    constructor(private data: DataService, private router: Router, private route: ActivatedRoute) {
        this.route.paramMap.subscribe(params => this.customerID = params.get('id'));
    }

    ngOnInit() {
        this.total = this.data.total;
    }

    onConfirm(): void {
        this.router.navigate(["home"]);
    }
}
