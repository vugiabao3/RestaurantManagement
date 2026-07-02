import axiosClient from "./axiosClient";

import type {

    RevenueResponse

} from "../types/report";

export const getRevenueReport=(year:number)=>{

    return axiosClient.get<RevenueResponse>(

        `/reports/revenue/${year}`

    );

};