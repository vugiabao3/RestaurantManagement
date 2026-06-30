import { useEffect, useState } from "react";
import {
    getPendingOrders,
    updateOrderStatus
} from "../../api/orderApi";

export default function ChefOrderPage() {

    const [orders, setOrders] = useState([]);

    useEffect(() => {
        load();
    }, []);

    const load = async () => {
        const res = await getPendingOrders();
        setOrders(res.data);
    };

    const handleReady = async (id: number) => {
        await updateOrderStatus({
            orderId: id,
            status: "Ready"
        });

        load();
    };

    return (
        <div>

            <h1>Đơn chờ xử lý</h1>

            {orders.map((o: any) => (
                <div key={o.orderId}>

                    <p>Order #{o.orderId}</p>
                    <p>{o.totalAmount}</p>

                    <button onClick={() => handleReady(o.orderId)}>
                        Đã làm xong
                    </button>

                </div>
            ))}

        </div>
    );
}