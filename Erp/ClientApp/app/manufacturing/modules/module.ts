export interface Products {
    productid: string;
    productname: string;
    BillofMatrialname: string;
    costofmanufacturing: number;
}


export interface BillofMatrials {
    BillofMatrialid: number;
    name: number;
    validFrom: string;
    validUntill: string;
    quantity: number;
    price: number;

}

export interface manufacturingOrders {
    manufacturingOrderid: string;
    productname: string;
    deadlinestart: string;
    quantity: number;
    rawmaterilasStatus: string;
    orderStatus: string;
}