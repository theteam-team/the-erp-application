import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from '../models/customerModel';
import { Router, ActivatedRoute } from '@angular/router';
import { customerService } from '../customers/customer.service';
import { DataService } from '../../shared/dataService';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'app-create-customer',
    templateUrl: './create-customer.component.html',
    styles: []
})
export class CreateCustomerComponent implements OnInit {



    Title: string;
    dateOfBirth: Date;
    customer: Customer;
    title: string;
    constructor(private _customerService: customerService, private _router: Router, private _route: ActivatedRoute, private data: DataService) { }

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
                is_lead: false,
                dateOfBirth: null,
                loyality_points: null,
                type: null
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

    saveInventory(CForm: NgForm): void {
        this.data.saveCustomer(CForm.value);
       // this.reloadComponent();
    }
}