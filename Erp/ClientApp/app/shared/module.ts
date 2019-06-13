export interface Module {
    id: number;
    title: string;
    category: string;
    discription: string;
    price: number;
} 
export interface Products {
    productid: string;
    unitsSold: number;
    profit: number;
    price: number;
    cost: number;
}
export interface Invoice {
    suppId: string;
    suppName: string;
    suppPhone: number;
    suppMail: string;
    payment_method: string;
    productName: string;
    productCost: number;
    suppUnits: number;
    totalCost: number;
    totalPaid: number;
    depts: number;
}