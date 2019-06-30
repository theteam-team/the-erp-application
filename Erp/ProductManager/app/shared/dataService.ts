import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from "rxjs/operators";

@Injectable()
export class DataService {

    constructor(private http: HttpClient) { }

    public availableProducts = [];
    loadAvailableProducts(): Observable<boolean> {
        return this.http.get("GetProductStore")
            .pipe(
                map((data: any[]) => {
                this.availableProducts = data;
                    return true;
                }));
    }

    addOrder(order) {
        return this.http.post("AddPotentialOrder", order).subscribe((data) => { });
    }

    addToOrder(product) {
        return this.http.post("AddProductToOrder", product).subscribe((data) => { });
    }
}