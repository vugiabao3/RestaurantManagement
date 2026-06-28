import { Link }
from "react-router-dom";

import "../../styles/layout/sidebar.css";

export default function Sidebar() {

    return (

        <div className="sidebar">

            <h2>
                Restaurant
            </h2>

            <Link to="/dashboard">
                Trang chủ
            </Link>

            <Link to="/foods">
                Món ăn
            </Link>

            <Link to="/categories">
                Danh mục
            </Link>

            <Link to="/menus">
                Menu
            </Link>

            <Link to="/reports">
                Báo cáo
            </Link>

        </div>
    );
}