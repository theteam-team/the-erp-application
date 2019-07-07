import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs'
import { HttpClient } from '@angular/common/http';
import { manufacturingOrders } from '../modules/module';

@Injectable()

export class ManufacturingOrderService {
  constructor(private httpClient: HttpClient) { }


  getOrders(): Observable<manufacturingOrders[]> {
    return this.httpClient.get<manufacturingOrders[]>('http://localhost:5000/manufacturingOrders');
  }


  saveProduct(order) {
    return this.httpClient.post<any>('http://localhost:5000/manufacturingOrders', order).subscribe((data) => { });
  }
}