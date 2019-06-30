import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DataService } from 'ClientApp/app/shared/dataService';

@Component({
  selector: 'app-reporting',
  templateUrl: './reporting.component.html',
  styleUrls: ['./reporting.component.css']
})
export class ReportingComponent implements OnInit {

    public report;

    constructor(private data: DataService, private router: Router) {

    }

    ngOnInit(): void {
        this.data.reporting()
            .subscribe(success => {
                if (success) {
                    this.report = this.data.report;
                }
            })
    }

}
