import { Injectable } from '@angular/core';
import { Customer } from '../models/customerModel';
import { Opportunity } from '../models/opportunityModel';
import { Observable , of} from 'rxjs';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
 
 
@Injectable()
export class customerService {

  constructor(private httpClient: HttpClient) { }

  private CustomersComponent: Customer[] = [
    {
      id: 1,
      first_name: 'menna',
      middle_name: 'essam',
      last_name: 'eldin',
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
      first_name: 'ahmed',
      middle_name: 'essam',
      last_name: 'eldin',
      email: 'menn6667@gmail.com',
      phone_number: '01144472565',
      company: 'none',
      company_email: 'sss@yahoo.com',
      gender: 'male',
      is_lead: true,
      dateOfBirth: new Date('1996-7-31'),
    }
  ];
  private PipelineComponent: Opportunity[] = [
    {
      id: 1,
      title: 'one',
      customer_id: 7,
      customer_name: 'menna',
      employee_id: 3,
      expected_revenue: 200,
      status: 1,
      start_date: new Date('31/7/1996'),
      end_date: new Date('31/8/1996'),
    },
    {
      id: 2,
      title: 'two',
      customer_id: 7,
      customer_name: 'ahmed',
      employee_id: 3,
      expected_revenue: 800,
      status: 1,
      start_date: new Date('3/7/1996'),
      end_date: new Date('31/7/1996'),
    }
  ];

  

  getCustomers(): Observable<Customer[]> {
    return this.httpClient.get<Customer[]>('http://localhost:3000/Customers')
      
  }

  getCustomer(id:number): Customer {
    return this.CustomersComponent.find(e => e.id === id);
  }

  saveCustomer(customer: Customer): Observable<Customer> {
    if (customer.id === null) {
      return this.httpClient.post <Customer>('http://localhost:3000/Customers', customer, {
        headers: new HttpHeaders({
          'content-type':'application/json'
        })
      })
     
    }
    else {
      const foundIndex = this.CustomersComponent.findIndex(e => e.id === customer.id);
      this.CustomersComponent[foundIndex] = customer;
    }
  }

  getOpportunities(): Observable<Opportunity[]> {
    return of( this.PipelineComponent);
  }
  getOpportunity(id:number): Opportunity {
    return this.PipelineComponent.find(e => e.id === id);
  }
  saveOpportunity(opportunity: Opportunity) {
    if (opportunity.id === null) {
      const maxId = this.PipelineComponent.reduce(function (e1, e2) {
        return (e1.id > e2.id) ? e1 : e2;
      }).id;
      opportunity.id = maxId + 1;
      this.PipelineComponent.push(opportunity)
    }
    else {
      const foundIndex = this.PipelineComponent.findIndex(e => e.id === opportunity.id);
      this.PipelineComponent[foundIndex] = opportunity;
    }
  }

  deleteCustomer(id: number) {
    const i = this.CustomersComponent.findIndex(e => e.id === id);
    if (i !== -1) {
      this.CustomersComponent.splice(i, 1);
    }
  }

  deleteOpportunity(id: number) {
    const i = this.PipelineComponent.findIndex(e => e.id === id);
    if (i !== -1) {
      this.PipelineComponent.splice(i, 1);
    }
  }
}
