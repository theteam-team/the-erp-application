import { Injectable } from '@angular/core';
import { Products } from '../modules/module';
import { Observable, of } from 'rxjs'
import { HttpClient } from '@angular/common/http';

@Injectable()

export class ProductsService {

    constructor(private httpClient: HttpClient) { }

    private product: Products[];

    getProducts(): Observable<Products[]> {
        return this.httpClient.get<Products[]>('http://localhost:3000/products');
    }
}