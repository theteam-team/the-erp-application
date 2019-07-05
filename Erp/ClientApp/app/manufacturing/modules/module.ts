export interface Products {
    productid: string;
    productname: string;
    BillofMatrialname: string;
    costofmanufacturing: string;
}


export interface BillofMatrials {
    BillofMatrialid: number;
    name: number;
    validFrom: string;
    validUntil: string;
    quantity: number;
    price: string;

}

export interface manufacturingOrders {
    manufacturingOrderid: string;
    productname: string;
    deadlinestart: string;
    quantity: number;
    rawmaterilasStatus: string;
    orderStatus: string;
}