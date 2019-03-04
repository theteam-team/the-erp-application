//import { HttpClient } from 'selenium-webdriver/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from "rxjs/operators";

@Injectable ()

export class DataService {
    constructor(private http: HttpClient) {

    }
    public modules = [];

    loadModules() {
        return this.http.get("/api/modules")
            .pipe(
            map((data: any[]) => {
                this.modules = data;
                return true;
            }));
            
    }
}