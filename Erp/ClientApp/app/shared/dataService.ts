// import { HttpClient } from 'selenium-webdriver/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
// import 'rxjs/add/operator/map';
@Injectable()

export class DataService {

    constructor(private http: HttpClient) {
    }
    public modules = [{
        title: "First Module",
        price: 19.6
    }, {
        title: "First Module",
        price: 19.6
    }, {
        title: "First Module",
        price: 19.6
    }];
}