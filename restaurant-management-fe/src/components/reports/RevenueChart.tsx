import {

BarChart,
Bar,
XAxis,
YAxis,
Tooltip,
CartesianGrid,
ResponsiveContainer

} from "recharts";

import type {

RevenueMonth

} from "../../types/report";

interface Props{

    data:RevenueMonth[];

}

export default function RevenueChart({

    data

}:Props){

    return(

        <div className="chart-card">

            <ResponsiveContainer

                width="100%"

                height={450}

            >

                <BarChart data={data}>

                    <CartesianGrid strokeDasharray="3 3"/>

                    <XAxis dataKey="month"/>

                    <YAxis/>

                    <Tooltip/>

                    <Bar

                        dataKey="revenue"

                        radius={[8,8,0,0]}

                    />

                </BarChart>

            </ResponsiveContainer>

        </div>

    );

}