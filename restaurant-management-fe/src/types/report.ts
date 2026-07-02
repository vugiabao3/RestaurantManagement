export interface RevenueMonth {

    month:number;

    revenue:number;

}

export interface RevenueResponse{

    year:number;

    totalRevenue:number;

    months:RevenueMonth[];

}