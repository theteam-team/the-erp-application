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
    cost: number;
    price: number;
    profit: number;
    
    
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

export interface Customer {
    customer_id: string;
    name: string;
    phone_number: number;
    email: string;
    DateOfBirth: string;
    gender: string;
    loyality_points: number;
    type: number;
    company: string;
    company_email: string;
    is_lead: boolean;
}

export interface customerOrders{
    id: string;
    requiredDate: string;
    completedDate: string;
    orderStatus: string;
    paymentID: string;
}

export interface OrderProducts{
    id: string;
    name: string;
    unitsInOrder: number;
    price: number;
    totalPrice: number;


}

export interface Account {
    account_id: string;
    account_money: number;
    creation_date: string;
    account_debts: number;
}