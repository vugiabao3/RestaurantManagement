import { useState } from "react";
import { useNavigate } from "react-router-dom";

import { previewPayment, checkout } from "../../api/paymentApi";

import PaymentPreview from "../../components/cashier/PaymentPreview";
import MemberInfoCard from "../../components/cashier/MemberInfoCard";
import PaymentMethodSelector from "../../components/cashier/PaymentMethodSelector";
import CheckoutButton from "../../components/cashier/CheckoutButton";

import type { PaymentPreview as Preview } from "../../types/payment";

import "../../styles/cashier/payment.css";

export default function PaymentPage() {

    const navigate = useNavigate();

    const [orderId, setOrderId] = useState("");
    const [preview, setPreview] = useState<Preview | null>(null);

    const [loading, setLoading] = useState(false);
    const [checkoutLoading, setCheckoutLoading] = useState(false);

    const [paymentMethod, setPaymentMethod] = useState("Cash");

    // ========================
    // PREVIEW BILL
    // ========================
    const handlePreview = async () => {

        if (!orderId) {
            alert("Nhập Order ID");
            return;
        }

        try {
            setLoading(true);

            const res = await previewPayment(Number(orderId));

setPreview({
    ...res.data,
    hasMemberCard: Boolean(res.data.hasMemberCard)
});
        } catch (err) {

            console.log(err);
            alert("Không tìm thấy order");

        } finally {
            setLoading(false);
        }
    };

    // ========================
    // CHECKOUT
    // ========================
    const handleCheckout = async () => {

        if (!preview) return;

        try {
            setCheckoutLoading(true);

            const memberId = Number(localStorage.getItem("memberId") ?? 0);

            const res = await checkout({
                orderId: preview.orderId,
                memberId,
                paymentMethod
            });

            alert(
                `Thanh toán thành công\n` +
                `Invoice: ${res.data.invoiceId}\n` +
                `${res.data.message}`
            );

            // reset UI
            setPreview(null);
            setOrderId("");

        } catch (err) {

            console.log(err);
            alert("Thanh toán thất bại");

        } finally {
            setCheckoutLoading(false);
        }
    };

    // ========================
    // UI
    // ========================
    return (
        <div className="payment-page">

            <h1>💰 Thanh toán</h1>

            {/* INPUT ORDER ID */}
            <div className="payment-box">

                <label>Order ID</label>

                <input
                    value={orderId}
                    onChange={(e) => setOrderId(e.target.value)}
                />

                <button onClick={handlePreview}>
                    {loading ? "Đang tải..." : "Xem hóa đơn"}
                </button>

            </div>

            {/* PREVIEW RESULT */}
            {preview && (

                <div className="payment-result">

                    <PaymentPreview data={preview} />

                    {/* KHÔNG CÓ THẺ */}
                    {!preview.hasMemberCard && (
                        <div className="member-box">

                            <p>Khách chưa có thẻ thành viên</p>

                            <button onClick={() => navigate("/members")}>
                                ➕ Đăng ký thành viên
                            </button>

                        </div>
                    )}

                    {/* CÓ THẺ */}
                    {preview.hasMemberCard && (
                        <MemberInfoCard preview={preview} />
                    )}

                    {/* CHỌN PHƯƠNG THỨC */}
                    <PaymentMethodSelector
                        value={paymentMethod}
                        onChange={setPaymentMethod}
                    />

                    {/* CHECKOUT */}
                    <CheckoutButton
                        loading={checkoutLoading}
                        onClick={handleCheckout}
                    />

                </div>
            )}

        </div>
    );
}