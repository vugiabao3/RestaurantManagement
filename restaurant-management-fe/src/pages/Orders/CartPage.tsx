import { useEffect, useState } from "react";
import {
    getCart,
    updateCartItem,
    deleteCartItem,
    clearCart
} from "../../api/orderApi";
import { placeOrder } from "../../api/orderApi";
import "../../styles/orders/cart.css";

export default function CartPage() {

    const [cart, setCart] = useState<any[]>([]);

    useEffect(() => {
        loadCart();
    }, []);

    // GET CART
    const loadCart = async () => {

        const userId = localStorage.getItem("userId");
        if (!userId) return;

        const res = await getCart(Number(userId));

        setCart(res.data.items || []);
    };

    // TĂNG
    const handleIncrease = async (item: any) => {

        await updateCartItem({
            orderItemId: item.orderItemId,
            quantity: item.quantity + 1
        });

        loadCart();
    };

    // GIẢM
    const handleDecrease = async (item: any) => {

        if (item.quantity <= 1) return;

        await updateCartItem({
            orderItemId: item.orderItemId,
            quantity: item.quantity - 1
        });

        loadCart();
    };

    // XÓA 1 MÓN
    const handleDelete = async (id: number) => {

        const ok = window.confirm("Bạn có muốn xóa món này không?");
        if (!ok) return;

        await deleteCartItem(id);

        alert("Đã xóa món");

        loadCart();
    };

    // XÓA TOÀN BỘ
    const handleClear = async () => {

        const userId = localStorage.getItem("userId");
        if (!userId) return;

        const ok = window.confirm("Bạn có chắc muốn xóa toàn bộ giỏ hàng?");
        if (!ok) return;

        await clearCart(Number(userId));

        alert("Đã xóa toàn bộ giỏ hàng");

        loadCart();
    };
    const handlePlaceOrder = async () => {

    const userId = localStorage.getItem("userId");

    if (!userId) {
        alert("Không tìm thấy userId");
        return;
    }

    const ok = window.confirm("Bạn chắc chắn muốn đặt món không?");
    if (!ok) return;

    try {

        await placeOrder(Number(userId));

        alert("🟢 Đặt món thành công! Chef đã nhận order");

        // reset cart UI
        setCart([]);

    } catch (err) {

        console.log(err);
        alert("❌ Đặt món thất bại");
    }
};

    // TOTAL
    const total = cart.reduce(
        (sum, i) => sum + i.subTotal,
        0
    );

    return (
        <div className="cart-page">

            <h1>🛒 Giỏ hàng của bạn</h1>

            {/* EMPTY */}
            {cart.length === 0 && (
                <p>Chưa có món nào trong giỏ</p>
            )}

            {/* LIST */}
            {cart.map(item => (
                <div key={item.orderItemId} className="cart-item">

                    {/* TÊN MÓN */}
                    <div className="cart-name">
                        🍽 {item.dishName}
                    </div>

                    {/* GIÁ */}
                    <div className="cart-price">
                        {item.unitPrice.toLocaleString()} đ
                    </div>

                    {/* QUANTITY CONTROL */}
                    <div className="cart-actions">

                        <button onClick={() => handleDecrease(item)}>
                            ➖
                        </button>

                        <span>
                            {item.quantity}
                        </span>

                        <button onClick={() => handleIncrease(item)}>
                            ➕
                        </button>

                    </div>

                    {/* SUBTOTAL */}
                    <div className="cart-subtotal">
                        {item.subTotal.toLocaleString()} đ
                    </div>

                    {/* DELETE */}
                    <button
                        className="delete-btn"
                        onClick={() => handleDelete(item.orderItemId)}
                    >
                        ❌
                    </button>

                </div>
            ))}

            {/* FOOTER */}
            {cart.length > 0 && (
                <div className="cart-summary">

                    <h3>
                        Tổng tiền: {total.toLocaleString()} đ
                    </h3>

                     <button
            className="place-btn"
            onClick={handlePlaceOrder}
        >
            🟢 Đặt món
        </button>

                    <button
                        className="clear-btn"
                        onClick={handleClear}
                    >
                        🗑 Xóa toàn bộ
                    </button>

                </div>
            )}

        </div>
    );
}