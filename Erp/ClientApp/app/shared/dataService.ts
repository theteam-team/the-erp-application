// import { HttpClient } from 'selenium-webdriver/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { Module } from './module';
// import 'rxjs/add/operator/map';
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

    /*public products = [{
        id: "1",
        name: "first",
        price: 10,
        units: 4
    }, {
        id: "2",
        name: "second",
        price: 8,
        units: 3
    }, {
        id: "3",
        name: "third",
        price: 20,
        units: 6
    }, {
        id: "4",
        name: "fourth",
        price: 3,
        units: 15
    }, {
        id: "5",
        name: "fifth",
        price: 5,
        units: 3
    }];*/
    public products = [];
    loadProducts(): Observable<boolean> {
        return this.http.get("/api/WarehouseApi/ShowProducts")
            .pipe(
                map((data: any[]) => {
                    this.products = data;
                    return true;
                }));
    }
}