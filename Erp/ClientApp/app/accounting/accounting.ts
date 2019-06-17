import { Component, OnInit } from '@angular/core';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';

@Component({
    selector: 'accounting-modules',
    templateUrl: 'accounting.html',
    styleUrls: ["accounting.css"],
})

export class AccountingModule{
    title = 'Accounting';
   
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
