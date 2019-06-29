import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

import { DataService } from 'ProductManager/app/shared/dataService';


@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

    constructor(private data: DataService, private router: Router) { }

    ngOnInit() {

    }

}
