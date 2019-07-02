 
import { Injectable } from '@angular/core';
import { Product } from '../models/productModel';
import { Observable, of } from 'rxjs'
import { HttpClient } from '@angular/common/http';

@Injectable()

export class ProductService {

  constructor(private httpClient: HttpClient) { }

  private product: Product[] ;

  getProducts(): Observable<Product[]>{
    return this.httpClient.get<Product[]>('http://localhost:3000/product');
  }
}
