import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from "rxjs/operators";
import { Module, Products } from './module';

@Injectable()
export class DataService {

    constructor(private http: HttpClient) { }

    private token: string = "";
    private tokenExpiration: Date;

    public modules: Module[] = [];
    loadModules(): Observable<boolean> {
        return this.http.get("/api/Module/GetModules/kemo")
            .pipe(
                map((data: any[]) => {
                    this.modules = data;
                    return true;
                }));
    }

    public get loginRequired(): boolean {
        return this.token.length == 0 || this.tokenExpiration > new Date();
    }
    

    public products = [];
    loadProducts(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowProducts")
            .pipe(
                map((data: any[]) => {
                    this.products = data;
                    return true;
                }));
    }

    public productsInOrder = [];
    loadProductsInOrder(id: string): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowProductsInOrder/" + id)
            .pipe(
                map((data: any[]) => {
                    this.productsInOrder = data;
                    return true;
                }));
    }

    searchProducts(key: string, value: string): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/SearchProducts/" + key + "/" + value)
            .pipe(
                map((data: any[]) => {
                    this.products = data;
                    return true;
                }));
    }

    searchOrders(key: string, value: string): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/SearchOrders/" + key + "/" + value)
            .pipe(
                map((data: any[]) => {
                    this.orders = data;
                    return true;
                }));
    }

    public productInfo;
    loadProductInfo(productID: string): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/GetProductById/" + productID)
            .pipe(
                map((data: any[]) => {
                    this.productInfo = data;
                    return true;
                }));
    }

    public orders = [];
    loadAllOrders(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowAllOrders")
            .pipe(
                map((data: any[]) => {
                    this.orders = data;
                    return true;
                }));
    }

    loadCompletedOrders(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowCompletedOrders")
            .pipe(
                map((data: any[]) => {
                    this.orders = data;
                    return true;
                }));
    }

    loadOrdersInProgress(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowOrdersInProgress")
            .pipe(
                map((data: any[]) => {
                    this.orders = data;
                    return true;
                }));
    }

    loadReadyOrders(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowReadyOrders")
            .pipe(
                map((data: any[]) => {
                    this.orders = data;
                    return true;
                }));
    }

    public orderInfo;
    loadOrderInfo(orderID: string): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/GetOrderInfo/" + orderID)
            .pipe(
                map((data: any[]) => {
                    this.orderInfo = data;
                    return true;
                }));
    }

    saveOrder(form) {
        return this.http.post("/api/WarehouseApi/AddOrder", form).subscribe((data) => { });
    }

    saveProduct(form) {
        return this.http.post("/api/WarehouseApi/AddProduct", form).subscribe((data) => { });
    }

    addProductToOrder(product) {
        return this.http.post("/api/WarehouseApi/AddProductToOrder", product).subscribe((data) => { });
    }

    deleteProductFromOrder(oID: string, pID: string) {
        return this.http.delete("/api/WarehouseApi/DeleteProductFromOrder/" + oID + "/" + pID).subscribe((data) => { });
    }

    saveEdits(form) {
        return this.http.put("/api/WarehouseApi/EditProduct", form).subscribe((data) => { });
    }

    saveOrderEdits(form) {
        return this.http.put("/api/WarehouseApi/EditOrder", form).subscribe((data) => { });
    }

    saveProductEdits(form) {
        return this.http.put("/api/WarehouseApi/EditProductInOrder", form).subscribe((data) => { });
    }

    deleteProduct(productID: string) {
        return this.http.delete("/api/WarehouseApi/DeleteProductById/" + productID).subscribe((data) => { });
    }

    deleteOrder(orderID: string) {
        return this.http.delete("/api/WarehouseApi/DeleteOrderById/" + orderID).subscribe((data) => { });
    }

    removeFromStock(form) {
        return this.http.put("/api/WarehouseApi/RemoveFromStock", form).subscribe((data) => { });
    }

    //Accounting
    public sold_products: Products[] = [];
    loadSoldProducts(): Observable<boolean> {
        return this.http.get("/api/AccountingApi/GetSoldProduct")
            .pipe(
                map((data: any[]) => {
                    this.sold_products = data;
                    return true;
                }));
    }
}
