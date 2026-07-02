import type { PaymentPreview } from "../../types/payment";

interface Props{

    data:PaymentPreview;

}

export default function PaymentPreview({ data }: Props) {

    const discount = data.discount ?? ((data.loyaltyPoints ?? 0) * 1000);

    const finalAmount =
        data.finalAmount ?? (data.total - discount);

    return (
        <div className="preview-card">

            <h2>🧾 Hóa đơn</h2>

            <div className="preview-row">
                <span>Order ID</span>
                <b>{data.orderId}</b>
            </div>

            <div className="preview-row">
                <span>Tổng tiền</span>
                <b>{data.total?.toLocaleString() ?? 0} đ</b>
            </div>

            <div className="preview-row">
                <span>Giảm giá</span>
                <b>{discount.toLocaleString()} đ</b>
            </div>

            <div className="preview-row">
                <span>Thành tiền</span>
                <b className="final">
                    {finalAmount.toLocaleString()} đ
                </b>
            </div>

            <hr />

            <div className="preview-row">
                <span>Có thẻ thành viên</span>
                <b>{data.hasMemberCard ? "Có" : "Không"}</b>
            </div>

            <div className="preview-row">
                <span>Tên thành viên</span>
                <b>{data.memberName ?? "--"}</b>
            </div>

            <div className="preview-row">
                <span>Điểm hiện tại</span>
                <b>{data.loyaltyPoints ?? 0}</b>
            </div>

            <div className="preview-row">
                <span>Mức giảm</span>
                <b>
                    {((data.loyaltyPoints ?? 0) * 1000).toLocaleString()} đ
                </b>
            </div>

        </div>
    );
}