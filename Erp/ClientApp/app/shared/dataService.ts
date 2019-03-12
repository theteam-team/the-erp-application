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

    public modules: Module[] = [];

    loadModules(): Observable<boolean> {
        return this.http.get("http://localhost:8888/api/Module/GetModules/new")
            .pipe(    
            map((data: any[]) => {
                    this.modules = data;
                    return true;
                }));
    }
}