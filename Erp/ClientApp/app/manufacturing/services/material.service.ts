import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs'
import { HttpClient } from '@angular/common/http';
import { BillofMatrials } from '../modules/module';

@Injectable()

export class MaterialService {

    constructor(private httpClient: HttpClient) { }

    private materials: BillofMatrials[];

    getMaterials(): Observable<BillofMatrials[]> {
        return this.httpClient.get<BillofMatrials[]>('http://localhost:3000/materials');
    }
}