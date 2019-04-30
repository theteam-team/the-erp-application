﻿import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/dataService';
import { Module } from "../shared/module";

@Component({
    selector: 'accounting-modules',
    templateUrl: 'accounting.html',
    styleUrls: ["accounting.css"],
})

export class AccountingModule implements OnInit {
    title = 'Accounting';
    constructor(private data: DataService) {
    }

    public modules: Module[] = [];

    ngOnInit(): void {
        this.data.loadModules()
            .subscribe(success => {
                if (success) {
                    this.modules = this.data.modules;
                }
            });
    }
    /*
    public addr = <HTMLSelectElement>document.getElementById('addr');
    public i: any = 1;
    public b: any ;
    private addRow(): void {
        alert("hi");
        this.b = this.i - 1;
        (this.addr + this.i)
    }
    */
}
