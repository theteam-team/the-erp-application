export interface Products {
    productid: string;
    productname: string;
    BillofMatrialId: number;
    costofmanufacturing: number;
}


export interface BillofMatrials {
    BillofMatrialid: number;
    name: number;
    validFrom: string;
    validUntil: string;
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
    responsible: string;
    material: string;
}

export interface createOrder {
    manufacturingOrderid: string;
    productname: string;
    deadlinestart: string;
    quantity: number;
    material: string;
    responsible: string;
}