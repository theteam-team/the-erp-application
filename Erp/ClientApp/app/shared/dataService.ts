﻿import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from "rxjs/operators";
import { Module, Products, Invoice, Customer, customerOrders, OrderProducts, Account } from './module';

@Injectable()
export class DataService {

    constructor(private http: HttpClient) { }

    private token: string = "";
    private tokenExpiration: Date;

    //CRM

    public opportunities = [];
    loadAllOpportunities(): Observable<boolean> {
        return this.http.get("/api/CrmApi/GetAllOpportunities")
            .pipe(
                map((data: any[]) => {
                    this.opportunities = data;
                    return true;
                }));
    }

    public customers = [];
    loadAllCustomers(): Observable<boolean> {
        return this.http.get("/api/CrmApi/GetAllCustomers")
            .pipe(
                map((data: any[]) => {
                    this.customers = data;
                    return true;
                }));
    }

    saveCustomer(form) {
        return this.http.post("/api/CrmApi/AddCustomer", form).subscribe((data) => { });
    }

    public Products = [];
    loadAllProducts(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowProducts")
            .pipe(
                map((data: any[]) => {
                    this.Products = data;
                    return true;
                }));
    }


    //warehouse

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

    public productsInInventory = [];
    showProductsInInventory(inventoryID: string): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/showProductsInInventory/" + inventoryID)
            .pipe(
                map((data: any[]) => {
                    this.productsInInventory = data;
                    return true;
                }));
    }

    public inventories = [];
    showInventories(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/showInventories/")
            .pipe(
                map((data: any[]) => {
                    this.inventories = data;
                    return true;
                }));
    }

    searchByCategory(value: string): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/SearchProducts/category/" + value)
            .pipe(
                map((data: any[]) => {
                    this.products = data;
                    return true;
                }));
    }

    searchInventories(key: string, value: string): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/SearchInventories/" + key + "/" + value)
            .pipe(
                map((data: any[]) => {
                    this.inventories = data;
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

    public report;
    reporting(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/Reporting")
            .pipe(
                map((data: any[]) => {
                    this.report = data;
                    return true;
                }));
    }

    public productsMoves = [];
    getProductsMoves(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/GetProductsMoves")
            .pipe(
                map((data: any[]) => {
                this.productsMoves = data;
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
        return this.http.get("/api/WarehouseApi/ShowDeliveries")
            .pipe(
                map((data: any[]) => {
                    this.orders = data;
                    return true;
                }));
    }

    loadReceipts(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowReceipts")
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

    loadCompletedReceipts(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowCompletedReceipts")
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

    loadWaitingOrders(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowWaitingOrders")
            .pipe(
                map((data: any[]) => {
                    this.orders = data;
                    return true;
                }));
    }

    loadWaitingReceipts(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowWaitingReceipts")
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

    saveInventory(form) {
        return this.http.post("/api/WarehouseApi/AddInventory", form).subscribe((data) => { });
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

    addProductToInventory(product) {
        return this.http.post("/api/WarehouseApi/AddProductToInventory", product).subscribe((data) => { });
    }

    saveEdits(form) {
        return this.http.put("/api/WarehouseApi/EditProduct", form).subscribe((data) => { });
    }

    editProductInInventory(form) {
        return this.http.put("/api/WarehouseApi/EditProductInInventory", form).subscribe((data) => { });
    }

    saveOrderEdits(form) {
        return this.http.put("/api/WarehouseApi/EditOrder", form).subscribe((data) => { });
    }

    saveProductEdits(form) {
        return this.http.put("/api/WarehouseApi/EditProductInOrder", form).subscribe((data) => { });
    }

    deleteInventory(inventoryID: string) {
        return this.http.delete("/api/WarehouseApi/DeleteInventory/" + inventoryID).subscribe((data) => { });
    }

    deleteProduct(productID: string) {
        return this.http.delete("/api/WarehouseApi/DeleteProductById/" + productID).subscribe((data) => { });
    }

    deleteOrder(orderID: string) {
        return this.http.delete("/api/WarehouseApi/DeleteOrderById/" + orderID).subscribe((data) => { });
    }

    deleteProductFromOrder(oID: string, pID: string) {
        return this.http.delete("/api/WarehouseApi/DeleteProductFromOrder/" + oID + "/" + pID).subscribe((data) => { });
    }

    deleteProductFromInventory(iID: string, pID: string) {
        return this.http.delete("/api/WarehouseApi/DeleteProductFromInventory/" + iID + "/" + pID).subscribe((data) => { });
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
    
    public invoice: Invoice[] = [];
    loadInvoice(): Observable<boolean> {
        return this.http.get("/api/AccountingApi/GetInvoice")
            .pipe(
                map((data: any[]) => {
                    this.invoice = data;
                    return true;
                }));
    }

    public customer: Customer[] = [];
    loadCustomerByID(id: string): Observable<boolean> {
        return this.http.get("/api/AccountingApi/GetCustomerById/" + id)
            .pipe(
                map((data: any[]) => {
                    this.customer = data;
                    return true;
                }));
    }
    public account: Account[] = [];
    loadCustomerAccount(id: string): Observable<boolean> {
        return this.http.get("/api/AccountingApi/GetCustomerAccount/" + id)
            .pipe(
                map((data: any[]) => {
                    this.account = data;
                    return true;
                }));
    }

    public customer_orders: customerOrders[] = [];
    loadCustomerOrders (id: string): Observable<boolean> {
        return this.http.get("/api/AccountingApi/getCustomerOrders/" + id)
            .pipe(
                map((data: any[]) => {
                this.customer_orders = data;
                    return true;
                }));
    }

    public order_products: OrderProducts[] = [];
    loadOrderProducts(id: string): Observable<boolean> {
        return this.http.get("/api/AccountingApi/getOrderProducts/" + id)
            .pipe(
                map((data: any[]) => {
                this.order_products = data;
                    return true;
                }));
    }

    
    
}
