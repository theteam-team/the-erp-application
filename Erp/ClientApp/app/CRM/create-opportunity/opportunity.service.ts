import { Injectable } from '@angular/core';
import { Opportunity } from '../models/opportunityModel';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
 
 

@Injectable()

export class OpportunityService {

  
  constructor(private httpClient: HttpClient) { }
 

  

  getOpportunities(): Observable<Opportunity[]> {
    return this.httpClient.get<Opportunity[]>('http://localhost:3000/opportunities')
  }
  getOpportunity(id: number): Observable< Opportunity>{
    return this.httpClient.get<Opportunity>('http://localhost:3000/opportunities/'+ id)
  }
  saveOpportunity(opportunity: Opportunity): Observable<Opportunity> {
    return this.httpClient.post<Opportunity>('http://localhost:3000/opportunities', opportunity, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      });
  }

  editOpportunity(opportunity: Opportunity): Observable<void> {
    return this.httpClient.put<void>('http://localhost:3000/opportunities/' + opportunity.id, opportunity, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
}

  deleteOpportunity(id: number): Observable<void> {
    return this.httpClient.delete<void>('http://localhost:3000/opportunities/' + id);
  
  }
}
