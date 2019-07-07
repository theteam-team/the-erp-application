import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
 
import { Opportunity } from '../models/opportunityModel';
import { OpportunityService } from './opportunity.service';
import { SalesmanService } from '../services/salesman.service';
import { DataService } from '../../shared/dataService';
import { ProductService } from '../services/product.service';
import { Product } from '../models/productModel';
import { MatDialog, MatDialogConfig } from '@angular/material'
import { ProductComponent } from '../../warehouse/product/product.component';
import { Salesman } from '../models/salesmanModel';
 

@Component({
    selector: 'app-create-opportunity',
    templateUrl: './create-opportunity.component.html',
    styles: []
})
export class CreateOpportunityComponent implements OnInit {
    public customers = [];
    products: Product[];
    Title: string;
     
    opportunity: Opportunity;
    salesman: Salesman[];
     

    constructor(private data: DataService, private _opportunityService: OpportunityService,
        private _router: Router, private _route: ActivatedRoute, private salesmanService: SalesmanService
        , private productService: ProductService, private dialog: MatDialog) { }

    ngOnInit() {
        this.displayAllCustomers();
        
        this.salesmanService.getSalesmans().subscribe(salesman => this.salesman = salesman);
        this.productService.getProducts().subscribe(products => this.products = products);

        this._route.paramMap.subscribe(parameterMap => {
            const id = +parameterMap.get('id');
            this.getOpportunity(id);
        });
    }
    displayAllCustomers(): void {

        this.data.loadAllCustomers()
            .subscribe(success => {
                if (success) {
                    this.customers = this.data.customers;
                }
            })
    }
    displayAllProducts(): void {

        this.data.loadAllProducts()
            .subscribe(success => {
                if (success) {
                    this.products = this.data.Products;
                }
            })
    }

    private getOpportunity(id: number) {
        if (id === 0) {
            this.opportunity = {
                id: null,
                title: null,
                customer_id: null,
                product_id: null,
                salesman_id: null,
                expected_revenue: null,
                quantity: null,
                status: 1,
                start_date: null,
                end_data: null,
                notes: null,

            };
            this.Title = 'Create an Opportunity';
        }
        else {
            this.Title = 'Update Opportunity';
            this._opportunityService.getOpportunity(id).subscribe(
                (opportunity) => this.opportunity = opportunity,
                (err) => console.log(err)
            );
        }
    }

    saveOpportunity(): void {
        if (this.opportunity.id == null) {
            this._opportunityService.saveOpportunity(this.opportunity).subscribe(
                (data: Opportunity) => {
                    console.log(data);
                    this._router.navigate([''])
                }
            );
        }
        else {
            this._opportunityService.editOpportunity(this.opportunity).subscribe(
                () => {
                    this._router.navigate([''])
                }
            );
        }
    }
    addProducts() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.width = "60%";
        this.dialog.open(ProductComponent, dialogConfig);
    }
}

