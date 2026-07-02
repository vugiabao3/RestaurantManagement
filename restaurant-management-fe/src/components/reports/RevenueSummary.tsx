import type {

    RevenueResponse

} from "../../types/report";

interface Props{

    data:RevenueResponse;

}

export default function RevenueSummary({

    data

}:Props){

    return(

        <div className="summary-card">

            <h2>

                Tổng doanh thu năm {data.year}

            </h2>

            <div className="summary-money">

                {data.totalRevenue.toLocaleString()} đ

            </div>

        </div>

    );

}