import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from '../models/customerModel';
import { Router, ActivatedRoute } from '@angular/router';
import { customerService } from '../customers/customer.service';
 

@Component({
  selector: 'app-create-customer',
  templateUrl: './create-customer.component.html',
  styles: []
})
export class CreateCustomerComponent implements OnInit {
  
  dateOfBirth: Date;
  customer: Customer ;
  title: string;
  constructor(private _customerService: customerService, private _router: Router, private _route: ActivatedRoute) { }

  ngOnInit() {
    this._route.paramMap.subscribe(params => {
      const id = +params.get('id');
      if (id) {
        this.getCustomer(id);
      }
    });
  }
   private getCustomer(id: number) {
    if (id === 0) {
      this.customer = {
        id: null,
        first_name: null,
        middle_name: null,
        last_name: null,
        email: '',
        phone_number: null,
        company: null,
        company_email: '',
        gender: null,
        is_lead: null,
        dateOfBirth: null,
      }
      this.title = 'Create New Customer';
    }
    else {
      this.customer = this._customerService.getCustomer(id);
      this.title = 'Update Customer';
    }
  }

  saveCustomer(): void {
    this._customerService.saveCustomer(this.customer).subscribe(
      (data: Customer) => {
        console.log(data);
        this._router.navigate(['customers'])
      });
   
  }
  

}
