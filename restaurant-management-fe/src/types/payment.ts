export interface PaymentPreview {

    orderId:number;

    total:number;

    discount:number;

    finalAmount:number;

    hasMemberCard:boolean;

    memberName:string | null;
loyaltyPoints: number;
}
export interface CheckoutRequest{

    orderId:number;

    memberId:number;

    paymentMethod:string;

}

export interface CheckoutResponse{

    invoiceId:number;

    total:number;

    discount:number;

    finalAmount:number;

    message:string;

}

export interface PaymentPreviewResponse {

    orderId: number;

    total: number;

    discount: number;

    finalAmount: number;

    hasMemberCard: boolean;

    memberName: string | null;

    loyaltyPoints: number;

}

export interface CheckoutRequest {

    orderId: number;

    memberId: number;

    paymentMethod: string;

}

export interface CheckoutResponse {

    invoiceId: number;

    total: number;

    discount: number;

    finalAmount: number;

    message: string;

}