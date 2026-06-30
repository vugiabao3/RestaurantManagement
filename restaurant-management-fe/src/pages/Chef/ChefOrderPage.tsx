import { useEffect, useState } from "react";
import {
    getChefOrders,
    updateOrderStatus
} from "../../api/orderApi";

import "../../styles/chef/chef.css";

export default function ChefOrderPage() {

    const [orders, setOrders] = useState<any[]>([]);

    useEffect(() => {
        loadOrders();
    }, []);

    // LOAD ALL ORDERS (NOT ONLY PENDING)
    const loadOrders = async () => {
        try {
            const res = await getChefOrders();

            // CHỈ LOẠI ORDER = READY (MẤT SAU KHI HOÀN THÀNH)
            const filtered = res.data.filter(
                (o: any) => o.status !== "Ready"
            );

            setOrders(filtered);

        } catch (err) {
            console.log(err);
        }
    };

    // UPDATE STATUS
    const handleUpdateStatus = async (orderId: number, status: string) => {

        try {

            await updateOrderStatus({
                orderId,
                status
            });

            alert(`Order ${orderId} → ${status}`);

            loadOrders();

        } catch (err) {
            console.log(err);
            alert("Cập nhật thất bại");
        }
    };

    return (
        <div className="chef-page">

            <h1>👨‍🍳 Chef Dashboard</h1>

            {orders.length === 0 && (
                <p>Không có đơn nào</p>
            )}

            {orders.map((order) => (
                <div key={order.orderId} className="order-card">

                    <h3>🧾 Order #{order.orderId}</h3>

                    <p>🪑 Table: {order.tableId}</p>

                    <p>📦 Status: <b>{order.status}</b></p>

                    {/* ITEMS */}
                    <div className="items">
                        {order.items.map((item: any) => (
                            <div key={item.orderItemId} className="item">
                                🍽 {item.dishName} × {item.quantity}
                            </div>
                        ))}
                    </div>

                    {/* ACTION BUTTONS */}
                    <div className="actions">

                        <button
                            className="btn-preparing"
                            onClick={() =>
                                handleUpdateStatus(order.orderId, "Preparing")
                            }
                        >
                            🔥 Preparing
                        </button>

                        <button
                            className="btn-ready"
                            onClick={() =>
                                handleUpdateStatus(order.orderId, "Ready")
                            }
                        >
                            ✅ Ready
                        </button>

                    </div>

                </div>
            ))}

        </div>
    );
}