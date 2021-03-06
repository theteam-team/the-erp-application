import { Component, OnInit } from '@angular/core';
 
import { Chart } from 'chart.js';
import { OpportunityService } from '../create-opportunity/opportunity.service';
import { Opportunity } from '../models/opportunityModel';

import { customerService } from '../customers/customer.service';
import { Salesman } from '../models/salesmanModel';
import { SalesmanService } from '../services/salesman.service';
import { DataService } from '../../shared/dataService';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styles: []
})
export class DashboardComponent implements OnInit {

    public customers = [];

    title = 'dashboard';
    chart;
    chart2;
    pie: any;
    doughnut: any;


    opportunities: Opportunity[];
   
    salesman: Salesman[];




    constructor(private _customerService: customerService, private _opportunityService: OpportunityService
        , private salesmanService: SalesmanService, private data: DataService) {


    }


    ngOnInit() {
        this.displayAllCustomers();
        this._opportunityService.getOpportunities().subscribe(opportunities => this.opportunities = opportunities);
        this.salesmanService.getSalesmans().subscribe(salesman => this.salesman = salesman);


        // this.ERNew().then(data => this.new = data);



        this.chart = new Chart('bar', {
            type: 'bar',
            options: {
                responsive: true,
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 10,
                        bottom: 10
                    }
                },
                title: {
                    display: true,
                    text: 'Opportunities'
                },
            },
            data: {
                labels: ['New', 'Qualified', 'Proposition', 'Negotiation', 'Won'],
                datasets: [
                    {
                        type: 'bar',
                        label: 'Expexted Revenue',
                        data: [1, 8, 45, 6, 2],
                        backgroundColor: 'darkorange',
                        borderColor: 'white',
                        fill: false,
                    }

                ]
            }
        });

        this.chart2 = new Chart('bar2', {
            type: 'bar',
            options: {
                responsive: true,
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 10,
                        bottom: 40
                    }
                },
                title: {
                    display: true,
                    text: 'Opportunities'
                },
            },
            data: {
                labels: ['New', 'Qualified', 'Proposition', 'Negotiation', 'Won'],
                datasets: [
                    {
                        type: 'bar',
                        label: 'count',
                        data: [3, 8, 45, 6, 2],
                        backgroundColor: 'darkorange',
                        borderColor: 'white',
                        fill: false,
                    }

                ]
            }
        });

        this.doughnut = new Chart('doughnut', {
            type: 'pie',
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Total Opportunities No.'
                }, legend: {
                    position: 'top',
                }, animation: {
                    animateScale: true,
                    animateRotate: true
                }
            },
            data: {
                datasets: [{
                    data: [45, 10, 5, 25, 15],
                    backgroundColor: [" chocolate", "olivedrab", "yellow", "darkcyan", " indianred"],
                    label: 'Opportunities'
                }],
                labels: ['New', 'Qualified', 'Proposition', 'Negotiation', 'Won']
            }
        })

        this.doughnut = new Chart('wonLoss', {
            type: 'pie',
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: ' Opportunities'
                }, legend: {
                    position: 'top',
                }, animation: {
                    animateScale: true,
                    animateRotate: true
                }
            },
            data: {
                datasets: [{
                    data: [0, 1],
                    backgroundColor: [" darkgreen", "darkred"],
                    label: 'Opportunities'
                }],
                labels: ['Won', 'Loss ']
            }
        })
    }

    displayAllCustomers(): void {

        this.data.loadAllCustomers()
            .subscribe(success => {
                if (success) {
                    this.customers = this.data.customers;
                }
            })
    }


    loss() {
        let loss = 0;
        loss = this._opportunityService.lossOpportunity();
        return loss;
    }
    Won() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 5) {
                total++;
            }
        }
        return total;
    }

    ERNew() {
        let total = 0;

        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 1) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }

    totalNew() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 1) {
                total++;
            }
        }
        return total;
    }

    ERQualified() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 2) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }

    totalQualified() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 2) {
                total++;
            }
        }
        return total;
    }

    ERProposition() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 3) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }

    totalProposition() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 3) {
                total++;
            }
        }
        return total;
    }

    ERNegotiation() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 4) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }

    totalNegotiation() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 4) {
                total++;
            }
        }
        return total;
    }

    ERWon() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 5) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }

    totalWon() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].status === 5) {
                total++;
            }
        }
        return total;
    }

    totalOpportunities() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            total++;
        }
        return total;
    }

    totalExpectedRevenue() {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            total += this.opportunities[i].expected_revenue;
        }
        return total;
    }

    CustomerNewER(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id && this.opportunities[i].status === 1) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }

    CustomerQualifiedER(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id && this.opportunities[i].status === 2) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }

    CustomerPropositionER(id: number) {
        let New = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id && this.opportunities[i].status === 3) {
                New += this.opportunities[i].expected_revenue;
            }
        }
        return New;
    }

    CustomerNegotiationER(id: number) {
        let New = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id && this.opportunities[i].status === 4) {
                New += this.opportunities[i].expected_revenue;
            }
        }
        return New;
    }

    CustomerWonER(id: number) {
        let New = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id && this.opportunities[i].status === 5) {
                New += this.opportunities[i].expected_revenue;
            }
        }
        return New;
    }

    CustomerTotalER(id: number) {
        let New = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id) {
                New += this.opportunities[i].expected_revenue;
            }
        }
        return New;
    }

    CustomerNewCount(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id && this.opportunities[i].status === 1) {
                total++;
            }
        }
        return total;
    }

    CustomerQualifiedCount(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id && this.opportunities[i].status === 2) {
                total++;
            }
        }
        return total;
    }

    CustomerPropositionCount(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id && this.opportunities[i].status === 3) {
                total++;
            }
        }
        return total;
    }

    CustomerNegotiationCount(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id && this.opportunities[i].status === 4) {
                total++;
            }
        }
        return total;
    }

    CustomerWonCount(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id && this.opportunities[i].status === 5) {
                total++;
            }
        }
        return total;
    }

    CustomerTotalCount(id: number) {
        let New = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].customer_id == id) {
                New++;
            }
        }
        return New;
    }
    SalesNewER(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].salesman_id == id && this.opportunities[i].status === 1) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }
    SalesQualifiedER(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].salesman_id == id && this.opportunities[i].status === 2) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }
    SalesPropositionER(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].salesman_id == id && this.opportunities[i].status === 3) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }
    SalesNegotiationER(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].salesman_id == id && this.opportunities[i].status === 4) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }
    SalesWonER(id: number) {
        let total = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].salesman_id == id && this.opportunities[i].status === 5) {
                total += this.opportunities[i].expected_revenue;
            }
        }
        return total;
    }
    SalesTotalER(id: number) {
        let New = 0;
        for (var i = 0; i < this.opportunities.length; i++) {
            if (this.opportunities[i].salesman_id == id) {
                New += this.opportunities[i].expected_revenue;
            }
        }
        return New;
    }
    totalCustomer() {
        let total = 0;
        for (var i = 0; i < this.customers.length; i++) {
            total++;
        }
        return total;
    }
}

  
