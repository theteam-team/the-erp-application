 
import { Injectable } from '@angular/core';
import { Product } from '../models/productModel';
import { Observable, of } from 'rxjs'

@Injectable()

export class ProductService {

  private product: Product[] = [
    {
      id: 678,
      name: "red ball",
      unit_price: 10,
      description: "ball",
      
    },
    {
      id: 944,
      name: "blue ball",
      unit_price: 20,
      description: "ball",
      
    },
    {
      id: 235,
      name: "green ball",
      unit_price: 30,
      description: "ball",
      
    }
  ];

  getProducts():Observable< Product[] >{
    return of(this.product);
  }
}
