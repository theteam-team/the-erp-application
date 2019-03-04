import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/dataService';

@Component({
    selector: 'module-list',
    templateUrl: 'moduleList.component.html',
    styleUrls: [],
})

export class ModuleList implements OnInit {

    constructor(private data: DataService) {
    }
    public modules = [];
    ngOnInit(): void {
        this.data.loadModules()
            .subscribe(success => {
                if (success) {
                    this.modules = this.data.modules;
                }
            })
    }
}