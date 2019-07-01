import { Component, OnInit } from '@angular/core';
import { Customer } from '../models/customerModel';
import { customerService } from '../services/customer.service';
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
    this._router.navigate(['/editCustomer', customerID])
  }
  deleteCustomer(customerID: number) {
    this._customerService.deleteOpportunity(customerID).subscribe(
      () => console.log('Customer deleted'),
      (err) => console.log(err)
    );
  }
}
/*
     this._customerService.getCustomers().subscribe( customers => this.customers = customers);

  }
  
  editCustomer(customerID: number) {
    this._router.navigate(['/editCustomer', customerID])
  }

  deleteCustomer(customerID: number) {
    this._customerService.deleteCustomer(customerID).subscribe(
      () => console.log('Customer with ID = ${customerID} deleted'),
      err => console.log(err)
    );
  }
 
}*/
