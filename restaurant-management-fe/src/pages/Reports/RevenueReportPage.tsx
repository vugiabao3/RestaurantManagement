import {

useEffect,
useState

} from "react";

import {

getRevenueReport

} from "../../api/reportApi";

import type {

RevenueResponse

} from "../../types/report";

import RevenueChart

from "../../components/reports/RevenueChart";

import RevenueSummary

from "../../components/reports/RevenueSummary";

import "../../styles/reports/revenueReport.css";

export default function RevenueReportPage(){

    const currentYear=

        new Date().getFullYear();

    const[year,setYear]=

        useState(currentYear);

    const[data,setData]=

        useState<RevenueResponse|null>(null);

    const[loading,setLoading]=

        useState(false);

    useEffect(()=>{

        loadReport();

    },[year]);

    const loadReport=async()=>{

        try{

            setLoading(true);

            const res=

            await getRevenueReport(year);

            setData(res.data);

        }

        catch(err){

            console.log(err);

            alert("Không tải được báo cáo");

        }

        finally{

            setLoading(false);

        }

    };

    return(

        <div className="report-page">

            <h1>

                📈 Báo cáo doanh thu

            </h1>

            <div className="year-box">

                <label>

                    Năm

                </label>

                <input

                    type="number"

                    value={year}

                    onChange={(e)=>

                        setYear(

                            Number(e.target.value)

                        )

                    }

                />

            </div>

            {

                loading &&

                <h3>

                    Đang tải...

                </h3>

            }

            {

                data &&

                <>

                    <RevenueSummary

                        data={data}

                    />

                    <RevenueChart

                        data={data.months}

                    />

                </>

            }

        </div>

    );

}