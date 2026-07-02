import axiosClient from "./axiosClient";

import type { PaymentPreview } from "../types/payment";
import type { CheckoutRequest } from "../types/payment";
import type { CheckoutResponse } from "../types/payment";

export const previewPayment = (

    orderId:number

) => {

    return axiosClient.get<PaymentPreview>(

        `/payments/preview/${orderId}`

    );

};

export const checkout=(

    data:CheckoutRequest

)=>{

    return axiosClient.post<CheckoutResponse>(
        "/payments/checkout",
        data
    );

};