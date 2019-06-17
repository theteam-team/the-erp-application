
import { Injectable } from '@angular/core';
import { Salesman } from '../models/salesmanModel';
import { Observable, of } from 'rxjs'

@Injectable()

export class SalesmanService {

  private salesman: Salesman[] = [
    {
      id: 1,
      name: "Rana",
      email: "rana@yahoo.com",
      phone_number: "01468873846",
      points: 0,
      Role_id: 0,
      gender: "female",
      is_Available: false,
      dateOfBirth: new Date("1996-08-2"),
      department: "sales"
    },
    {
      id: 2,
      name: "kamal",
      email: "kamal@yahoo.com",
      phone_number: "014609547985",
      points: 0,
      Role_id: 0,
      gender: " male",
      is_Available: false,
      dateOfBirth: new Date("1994-01-9"),
      department: "sales"
    },
    {
      id: 3,
      name: "Abdelrahman",
      email: "kiro@yahoo.com",
      phone_number: "012564878675",
      points: 0,
      Role_id: 0,
      gender: "male",
      is_Available: false,
      dateOfBirth: new Date("1996-03-5"),
      department: "sales"
    }
  ];

  getSalesmans(): Observable <Salesman[]> {
    return of(this.salesman);
  }
}
