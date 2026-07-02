import { useState } from "react";
import { registerMember } from "../../api/memberApi";
import "../../styles/cashier/member.css";
interface MemberRegisterFormProps {
    onSuccess?: () => void;
}
export default function MemberRegisterForm({
    onSuccess
}: MemberRegisterFormProps) {
    const [fullName, setFullName] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e: React.FormEvent) => {

        e.preventDefault();

        try {

            setLoading(true);

            const userId = Number(localStorage.getItem("userId"));

            const res = await registerMember({
                fullName,
                phoneNumber,
                userId
            });

            alert(
`Member ID: ${res.data.memberId}
Card: ${res.data.cardId}
${res.data.message}`
            );

            localStorage.setItem(
    "memberId",
    String(res.data.memberId)
);

setFullName("");
setPhoneNumber("");

// đóng popup
onSuccess?.();

        } catch (err: any) {

            console.log(err);

            alert(
                err.response?.data?.message ??
                "Đăng ký thất bại"
            );

        } finally {
            setLoading(false);
        }

    };

    return (

        <div className="member-container">

            <form className="member-form" onSubmit={handleSubmit}>

                <h2>👤 Đăng ký thành viên</h2>

                {/* FULL NAME */}
                <div className="form-group">

                    <label>Họ và tên</label>

                    <input
                        type="text"
                        placeholder="Nhập họ tên..."
                        value={fullName}
                        onChange={(e) =>
                            setFullName(e.target.value)
                        }
                        required
                    />

                </div>

                {/* PHONE */}
                <div className="form-group">

                    <label>Số điện thoại</label>

                    <input
                        type="text"
                        placeholder="Nhập số điện thoại..."
                        value={phoneNumber}
                        onChange={(e) =>
                            setPhoneNumber(e.target.value)
                        }
                        required
                    />

                </div>

                <button disabled={loading}>

                    {loading
                        ? "Đang đăng ký..."
                        : "Đăng ký"
                    }

                </button>

            </form>

        </div>

    );

}