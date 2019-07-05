import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs'
import { HttpClient } from '@angular/common/http';
import { manufacturingOrders } from '../modules/module';

@Injectable()

export class ManufacturingOrderService {
  constructor(private httpClient: HttpClient) { }

  private materials: manufacturingOrders[];

  getProducts(): Observable<manufacturingOrders[]> {
    return this.httpClient.get<manufacturingOrders[]>('http://localhost:3000/manufacturingOrders');
  }
}