import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from "rxjs/operators";

@Injectable()
export class DataService {

    constructor(private http: HttpClient) { }

    public availableProducts = [];
    loadAvailableProducts(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowAvailableProducts")
            .pipe(
                map((data: any[]) => {
                this.availableProducts = data;
                    return true;
                }));
    }

}