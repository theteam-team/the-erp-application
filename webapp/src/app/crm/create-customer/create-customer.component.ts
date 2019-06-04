import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from '../models/customerModel';
import { Router, ActivatedRoute } from '@angular/router';
import { customerService } from '../customers/customer.service';
import { NgForm } from '@angular/forms';
 

@Component({
  selector: 'app-create-customer',
  templateUrl: './create-customer.component.html',
  styles: []
})
export class CreateCustomerComponent implements OnInit {
  @ViewChild('customerForm') public createCustomerForm: NgForm;

  Title: string;
  dateOfBirth: Date;
  customer: Customer = {
    id: null,
    name: null,
    email: '',
    phone_number: null,
    company: null,
    company_email: '',
    gender: null,
    is_lead: null,
    dateOfBirth: null,
  };
  title: string;
  constructor(private _customerService: customerService, private _router: Router, private _route: ActivatedRoute) { }

  ngOnInit() {
    this._route.paramMap.subscribe(parameterMap => {
      const id = +parameterMap.get('id');
      this.getCustomer(id);
    });
  }

  private getCustomer(id: number) {
    if (id === 0) {
      this.customer = {
        id: null,
        name: null,
        email: '',
        phone_number: '',
        company: null,
        company_email: '',
        gender: null,
        is_lead: null,
        dateOfBirth: null,
      };
      this.Title = 'Create New Customer';
    }
    else {
      this.Title = 'Update Customer';
      this._customerService.getCustomer(id).subscribe(
        (customer) => this.customer = customer,
        (err) => console.log(err)
      );
    }
  }

  saveCustomer(): void {
    if (this.customer.id == null) {
      this._customerService.saveCustomer(this.customer).subscribe(
        (data: Customer) => {
          console.log(data);
          this._router.navigate([''])
        }
      );
    }
    else {
      this._customerService.editCustomer(this.customer).subscribe(
        () => {
          this._router.navigate(['customers'])
        }
      );
    }
  }
}
