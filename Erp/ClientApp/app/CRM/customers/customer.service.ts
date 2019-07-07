import { Injectable } from '@angular/core';
  
import { Customer } from  '../models/customerModel'; 
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from "rxjs/operators";


@Injectable()
export class customerService {

    constructor(private httpClient: HttpClient) { }

    

    /*public customers = [];
    getCustomers(): Observable<boolean> {
        return this.httpClient.get("/api/CrmApi/GetAllCustomers")
            .pipe(
                map((data: any[]) => {
                    this.customers = data;
                    return true;
                }));
    }*/

    getCustomer(id: number): Observable<Customer> {
        return this.httpClient.get<Customer>("/api/CrmApi/GetCustomer/{id}" + id)
    }
    saveCustomer(customer: Customer): Observable<Customer> {
        return this.httpClient.post<Customer>("/api/CrmApi/AddCustomer", customer, {
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

