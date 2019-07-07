import { Component, OnInit } from '@angular/core';
import { Customer } from '../models/customerModel';
import { customerService } from '../customers/customer.service';
import { Router } from '@angular/router';
import { DataService } from '../../shared/dataService';


@Component({
    selector: 'app-customers',
    templateUrl: './customers.component.html',
    styles: []
})
export class CustomersComponent implements OnInit {

    public customers = [];

    searchTerm: string;


    constructor(private _customerService: customerService, private _router: Router, private data: DataService) { }

    ngOnInit() {
        this.displayAllCustomers();
    }
    editCustomer(customerID: number) {
        this._router.navigate(['/crm/editCustomer', customerID])
    }
    deleteCustomer(customerID: number) {
        this._customerService.deleteOpportunity(customerID).subscribe(
            () => console.log('Customer deleted'),
            (err) => console.log(err)
        );
    }

    displayAllCustomers(): void {

        this.data.loadAllCustomers()
            .subscribe(success => {
                if (success) {
                    this.customers = this.data.customers;
                }
            })
    }
}
 
