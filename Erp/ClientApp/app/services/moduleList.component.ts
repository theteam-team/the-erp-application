import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/dataService';
import { Module } from "../shared/module";

@Component({
    selector: 'module-list',
    templateUrl: 'moduleList.component.html',
    styleUrls: [],
})

export class ModuleList implements OnInit {

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
}