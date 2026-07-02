import "../../styles/cashier/memberInfoCard.css";

interface Props {

    preview: {

        memberName: string | null;

        loyaltyPoints: number;

        discount: number;

        total: number;

        finalAmount: number;

    };

}

export default function MemberInfoCard({

    preview

}: Props) {

    return (

        <div className="member-info-card">

            <div className="member-title">

                ✅ Thành viên

            </div>

            <div className="member-row">

                <span className="label">

                    Tên khách

                </span>

                <span>

                    {preview.memberName}

                </span>

            </div>

            <div className="member-row">

                <span className="label">

                    Điểm tích lũy

                </span>

                <span>

                    {preview.loyaltyPoints} điểm

                </span>

            </div>

            <div className="member-row">

                <span className="label">

                    Tổng tiền

                </span>

                <span>

                    {preview.total.toLocaleString()} đ

                </span>

            </div>

            <div className="member-row">

                <span className="label">

                    Giảm giá

                </span>

                <span className="discount">

                    -

                    {preview.discount.toLocaleString()} đ

                </span>

            </div>

            <div className="member-row total">

                <span>

                    Thành tiền

                </span>

                <span>

                    {preview.finalAmount.toLocaleString()} đ

                </span>

            </div>

        </div>

    );

}