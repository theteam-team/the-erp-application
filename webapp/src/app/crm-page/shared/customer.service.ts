import { Injectable } from '@angular/core';
import { Customer } from './customer.model';
import { HttpClient } from 'selenium-webdriver/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  customerData: Customer;

  constructor(private httpClient: HttpClient) { }

  
}
