import { Component, OnInit } from '@angular/core';
import { customerService } from '../services/customer.service';
import { Customer } from '../models/customerModel';
import { Qoutation } from '../models/qoutationModel';
import { Salesman } from '../models/salesmanModel';
import { NgForm } from '@angular/forms';
import { QoutationService } from '../qoutation/qoutation.service';
import { Router, ActivatedRoute } from '@angular/router';
import { SalesmanService } from '../services/salesman.service';

@Component({
  selector: 'app-create-qoutation',
  templateUrl: './create-qoutation.component.html',
  styles: []
})
export class CreateQoutationComponent implements OnInit {
  salesman: Salesman[];
  customers: Customer[];
  Qoutation: Qoutation;

  constructor(private qoutationService: QoutationService, private router: Router, private route: ActivatedRoute, private salesmanService: SalesmanService) { }

  ngOnInit() {
    // this.CustomerService.getCustomers().subscribe(customers => this.customers = customers);
    this.salesmanService.getSalesmans().subscribe(salesman => this.salesman = salesman);

    this.route.paramMap.subscribe(parameterMap => {
      const id = +parameterMap.get('id');
      this.getQoutation(id)
    });
  }
  private getQoutation(id: number) {
    if (id === 0) {
      this.Qoutation = {
        id:null,
        customer_id: null,
        salesman_id: null,
        product_id: null,
        quantity: null,
        date: null,
      }
    }
    else {
      this.Qoutation = Object.assign({},this.qoutationService.getQoutation(id));
      }
    
  }

  saveQoutation(newQoutation: Qoutation): void{
    this.qoutationService.saveQoutation(this.Qoutation);
    this.router.navigate(['quotations']);
  }
}
