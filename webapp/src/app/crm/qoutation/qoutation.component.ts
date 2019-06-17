import { Component, OnInit } from '@angular/core';
import { Product } from '../models/productModel';
import { QoutationService } from './qoutation.service';
import { ProductService } from './product.service';
import { Salesman } from '../models/salesmanModel';
import { Qoutation } from '../models/qoutationModel';
import { SalesmanService } from './salesman.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-qoutation',
  templateUrl: './qoutation.component.html',
  styles: []
})
export class QoutationComponent implements OnInit {

  products: Product[] ;
  salesman: Salesman[];
  qoutations: Qoutation[];
  constructor(private productService: ProductService, private salesmanService: SalesmanService,
    private qoutationService: QoutationService, private router: Router) { }

  ngOnInit() {
    this.productService.getProducts().subscribe(products => this.products = products);
    this.salesmanService.getSalesmans().subscribe( salesman => this.salesman = salesman);
    this.qoutationService.getQoutations().subscribe(qoutations => this.qoutations = qoutations);
  }
  editQoutation(id: number) {
    this.router.navigate(['/editQoutation', id]);
  }
}
