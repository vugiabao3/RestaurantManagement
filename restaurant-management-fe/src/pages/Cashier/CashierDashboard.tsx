import { useState } from "react";
import MemberRegisterForm from "../../components/cashier/MemberRegisterForm";
import "../../styles/cashier/cashierDashboard.css";
import { useNavigate } from "react-router-dom";
export default function CashierDashboard() {

    const [openMemberForm, setOpenMemberForm] = useState(false);
const navigate = useNavigate();
    return (

        <div className="cashier-dashboard">

            <h1>💳 Cashier Dashboard</h1>

            <p>Chọn chức năng muốn sử dụng</p>

            <div className="cashier-menu">

                {/* MEMBER */}
                <div
                    className="cashier-card"
                    onClick={() => setOpenMemberForm(true)}
                >
                    <h2>👤 Đăng ký thành viên</h2>
                    <p>Tạo thẻ thành viên cho khách hàng</p>
                </div>

                {/* PAYMENT (chưa làm) */}
                <div
    className="cashier-card"
    onClick={() => navigate("/payments")}
>

    <h2>💰 Thanh toán</h2>

    <p>Thanh toán hóa đơn</p>

</div>

            </div>

            {/* POPUP FORM */}
            {openMemberForm && (
                <div className="modal-overlay">

                    <div className="modal-box">

                        <button
                            className="close-btn"
                            onClick={() => setOpenMemberForm(false)}
                        >
                            ✖
                        </button>

                        <MemberRegisterForm
                            onSuccess={() => setOpenMemberForm(false)}
                        />

                    </div>

                </div>
            )}

        </div>

    );

}