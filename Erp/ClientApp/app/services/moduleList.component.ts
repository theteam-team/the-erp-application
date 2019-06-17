import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/dataService';
import { Module } from "../shared/module";
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';

@Component({
    selector: 'module-list',
    templateUrl: 'moduleList.component.html',
    styleUrls: ["moduleList.component.css"],
})
export class ModuleList implements OnInit {
    title = 'ModuleList';
    constructor(private data: DataService , private router: Router) { }

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
    onAccounting() {
        if (this.data.loginRequired) {
            //force login
            this.router.navigate(["login"]);
        }
        else {
            //go to 
            this.router.navigate(["accounting"]);

        }
    }
    */
}