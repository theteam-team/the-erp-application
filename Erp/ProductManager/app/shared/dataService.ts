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

   addToOrder(product, order) {
       return this.http.post("AddToOrder", product).subscribe((data) => {});
    }

    addCustomerAddress(address) {
        return this.http.post("AddCustomerAddress", address).subscribe((data) => { });
    }

    public customerProducts = [];
    public total;
    loadCustomerProducts(id): Observable<boolean> {
        return this.http.get("GetCustomerProducts/" + id)
            .pipe(
                map((data: any[]) => {
                this.customerProducts = data;
                    return true;
                }));
    }

    deleteProductFromOrder(oID: string, pID: string) {
        return this.http.delete("DeleteCustomerProduct/" + oID + "/" + pID).subscribe((data) => { });
    }

    addToOrderTotal(form) {
        return this.http.put("AddToOrderTotal", form).subscribe((data) => { });
    }

    removeFromOrderTotal(form) {
        return this.http.put("RemoveFromOrderTotal", form).subscribe((data) => { });
    }

    public orderInfo;
    loadOrderInfo(orderID: string): Observable<boolean> {
        return this.http.get("GetOrder/" + orderID)
            .pipe(
                map((data: any[]) => {
                    this.orderInfo = data;
                    return true;
                }));
    }

    public customerID;
    getCustomerID(): Observable<boolean> {
        return this.http.get("GetUserId", { responseType: 'text' })
            .pipe(
                map((data: string) => {
                    this.customerID = data;
                    return true;
                }));
    }

    addPayment(payment, order) {
        this.http.post("AddPayment", payment, { responseType: 'text' }).toPromise().then(res => this.http.put("AddOrderPayment", order).subscribe((data) => { }))
            .catch(msg => console.log('Error: ' + msg.status + ' ' + msg.statusText));
       
    }
}