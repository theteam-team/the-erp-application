import { Component, OnInit } from '@angular/core';
import { Customer } from '../models/customerModel';
import { customerService } from '../customers/customer.service';
import { Router } from '@angular/router';


@Component({
    selector: 'app-customers',
    templateUrl: './customers.component.html',
    styles: []
})
export class CustomersComponent implements OnInit {

    searchTerm: string;
    customers: Customer[];


    constructor(private _customerService: customerService, private _router: Router) { }

    ngOnInit() {
        this._customerService.getCustomers().subscribe(customers => this.customers = customers);
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
}
 
