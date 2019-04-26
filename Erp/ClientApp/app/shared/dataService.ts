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
}