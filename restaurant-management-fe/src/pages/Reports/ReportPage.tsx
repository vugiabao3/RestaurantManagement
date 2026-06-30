import { useNavigate } from "react-router-dom";
import "../../styles/reports/reportPage.css";

export default function ReportPage() {
    const navigate = useNavigate();

    return (
        <div className="report-page">

            <h1>📊 Trung tâm báo cáo</h1>

            <div className="report-grid">

                <div
                    className="report-card food"
                    onClick={() => navigate("/reports/food")}
                >
                    <h2>📊 Báo cáo món ăn</h2>
                    <p>Thống kê Dish / Category / Menu</p>
                </div>

                <div
                    className="report-card revenue"
                    onClick={() => navigate("/reports/revenue")}
                >
                    <h2>💰 Báo cáo doanh thu</h2>
                    <p>Doanh thu theo tháng / ngày</p>
                </div>

            </div>

        </div>
    );
}