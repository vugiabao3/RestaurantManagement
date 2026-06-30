import {
    useNavigate
}
from "react-router-dom";

import "../../styles/layout/navbar.css";

export default function Navbar() {

    const navigate =
        useNavigate();

    const handleLogout =
        () => {

            localStorage.removeItem(
                "token"
            );
            localStorage.removeItem("role");

            navigate("/");
        };

    return (

        <div className="navbar">

            <h3>
                TRANG CHỦ
            </h3>

            <button
                onClick={
                    handleLogout
                }
            >
                ĐĂNG XUẤT
            </button>

        </div>
    );
}