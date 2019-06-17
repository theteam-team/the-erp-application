 
import { Injectable } from '@angular/core';
import { Qoutation } from '../models/qoutationModel';
import { Observable,of } from 'rxjs';
 

@Injectable()

export class QoutationService {
  private qoutations: Qoutation[] = [
    {
      id: 1,
      customer_id: 1,
      salesman_id: 1,
      product_id: 235,
      quantity: 3,
      date: new Date("2019-03-5"),
    },
    {
      id: 2,
      customer_id: 3,
      salesman_id: 2,
      product_id: 678,
      quantity: 2,
      date: new Date("2019-04-5"),
    },
    {
      id: 3,
      customer_id: 2,
      salesman_id: 3,
      product_id: 678,
      quantity: 2,
      date: new Date("2019-04-9"),
    }
  ]

  getQoutations(): Observable<Qoutation[]> {
    return of(this.qoutations);
  }
  getQoutation(id:number): Qoutation{
    return this.qoutations.find( e => e.id === id);
  }
  saveQoutation(qoutation: Qoutation) {
    this.qoutations.push(qoutation);
  }
}
