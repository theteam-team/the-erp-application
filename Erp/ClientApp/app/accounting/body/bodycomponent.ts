import { Component, OnInit } from '@angular/core';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';

@Component({
    selector: 'accounting-body',
    templateUrl: 'bodycomponent.html',
    styleUrls: ["bodycomponent.css"],
})

export class AccountingBody {
    title = 'Accounting';
}