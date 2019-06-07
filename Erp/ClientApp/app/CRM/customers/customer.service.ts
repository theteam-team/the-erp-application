import { Injectable } from '@angular/core';
import { Customer } from '../models/customerModel';
import { Opportunity } from '../models/opportunityModel';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';



@Injectable()
export class customerService {

    constructor(private httpClient: HttpClient) { }

    private customers: Customer[] = [
        {
            id: 1,
            name: 'menna',
            email: 'menna777@gmail.com',
            phone_number: '01117572565',
            company: 'none',
            company_email: 'mmm@yahoo.com',
            gender: 'female',
            is_lead: true,
            dateOfBirth: new Date('1996-1-31'),
        },
        {
            id: 2,
            name: 'ahmed',
            email: 'menn6667@gmail.com',
            phone_number: '01144472565',
            company: 'none',
            company_email: 'sss@yahoo.com',
            gender: 'male',
            is_lead: true,
            dateOfBirth: new Date('1996-7-31'),
        }
    ];

    getCustomers(): Observable<Customer[]> {
        return this.httpClient.get<Customer[]>('http://localhost:3000/customers')
    }
    getCustomer(id: number): Observable<Customer> {
        return this.httpClient.get<Customer>('http://localhost:3000/customers/' + id)
    }
    saveCustomer(customer: Customer): Observable<Customer> {
        return this.httpClient.post<Customer>('http://localhost:3000/customers', customer, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        });
    }

    editCustomer(customer: Customer): Observable<void> {
        return this.httpClient.put<void>('http://localhost:3000/customers/' + customer.id, customer, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        });
    }

    deleteOpportunity(id: number): Observable<void> {
        return this.httpClient.delete<void>('http://localhost:3000/customers/' + id);

    }
}

