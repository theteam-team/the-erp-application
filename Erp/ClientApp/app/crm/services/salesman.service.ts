
import { Injectable } from '@angular/core';
import { Salesman } from '../models/salesmanModel';
import { Observable, of } from 'rxjs'
import { HttpClient } from '@angular/common/http';

@Injectable()

export class SalesmanService {

  constructor(private httpClient: HttpClient) { }

  private salesman: Salesman[] ;

  getSalesmans(): Observable<Salesman[]> {
    return this.httpClient.get<Salesman[]>('http://localhost:3000/salesman');
  }
}
