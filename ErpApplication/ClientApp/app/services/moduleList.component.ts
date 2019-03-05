import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/dataService';

@Component({
    selector: 'module-list',
    templateUrl: 'moduleList.component.html',
    styleUrls: [],
})

export class ModuleList{
    constructor(private data: DataService) {
        this.modules = data.modules;
    }
    public modules = [];
}