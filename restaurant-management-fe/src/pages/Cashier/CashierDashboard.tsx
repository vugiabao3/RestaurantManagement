import { useState } from "react";
import { registerMember } from "../../api/memberApi";

export default function CashierDashboard() {

    const [open, setOpen] = useState(false);

    const [fullName, setFullName] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [userId, setUserId] = useState<number>(0);

    const [result, setResult] = useState<any>(null);

    const handleSubmit = async (e: React.FormEvent) => {

        e.preventDefault();

        try {

            const res = await registerMember({
                fullName,
                phoneNumber,
                userId
            });

            setResult(res.data);

            alert(
                `MemberID: ${res.data.memberId}
Card: ${res.data.cardId}
${res.data.message}`
            );

        } catch (err: any) {

            console.log(err);

            alert(err.response?.data?.message || "Lỗi đăng ký");

        }

    };

    return (

        <div className="cashier-dashboard">

            <h1>💳 Cashier Dashboard</h1>

            <p>Chọn chức năng muốn sử dụng</p>

            <div className="cashier-menu">

                {/* OPEN FORM BUTTON */}
                <button
                    className="cashier-card"
                    onClick={() => setOpen(true)}
                >
                    👤 Đăng ký thành viên
                </button>

                <button className="cashier-card">
                    💰 Thanh toán
                </button>

            </div>

            {/* MODAL FORM */}
            {open && (

                <div className="modal-overlay">

                    <div className="modal">

                        <h2>Đăng ký thành viên</h2>

                        <form onSubmit={handleSubmit}>

                            <input
                                placeholder="Full Name"
                                value={fullName}
                                onChange={(e) =>
                                    setFullName(e.target.value)
                                }
                            />

                            <input
                                placeholder="Phone Number"
                                value={phoneNumber}
                                onChange={(e) =>
                                    setPhoneNumber(e.target.value)
                                }
                            />

                            <input
                                placeholder="User ID"
                                type="number"
                                value={userId}
                                onChange={(e) =>
                                    setUserId(Number(e.target.value))
                                }
                            />

                            <div style={{ marginTop: 10 }}>

                                <button type="submit">
                                    Đăng ký
                                </button>

                                <button
                                    type="button"
                                    onClick={() => setOpen(false)}
                                >
                                    Hủy
                                </button>

                            </div>

                        </form>

                        {/* RESULT */}
                        {result && (

                            <div style={{ marginTop: 20 }}>

                                <h3>Kết quả</h3>

                                <p>MemberId: {result.memberId}</p>
                                <p>CardId: {result.cardId}</p>
                                <p>{result.message}</p>

                            </div>

                        )}

                    </div>

                </div>

            )}

        </div>

    );

}