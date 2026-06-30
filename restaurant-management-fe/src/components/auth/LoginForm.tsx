import { useState } from "react";
import "../../styles/auth/login.css";

import { login } from "../../api/authApi";
import { Link, useNavigate } from "react-router-dom";

export default function LoginForm() {

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {

        e.preventDefault();

        try {

            const response = await login({
                email,
                password
            });

            // DATA FROM API
            const {
    token,
    role,
    userId
} = response.data;

localStorage.setItem("token", token);
localStorage.setItem("role", role);
localStorage.setItem("userId", String(userId));

         

            // CUSTOMER ID
        

            alert("Đăng nhập thành công");

            // REDIRECT
            // REDIRECT
if (role === "Admin") {
    navigate("/dashboard");
} else if (role === "Chef") {
    navigate("/chef");
} else if (role === "Cashier") {
    navigate("/cashier");
} else if (role === "Customer") {
    navigate("/menus");
} else {
    navigate("/menus"); // fallback an toàn
}

        } catch (error) {

            console.error(error);

            alert("Đăng nhập thất bại");
        }
    };

    return (
        <div className="login-page">

            <div className="login-container">

                <h2 className="login-title">
                    Restaurant Management
                </h2>

                <p className="login-subtitle">
                    Admin Login
                </p>

                <form
                    className="login-form"
                    onSubmit={handleSubmit}
                >

                    <input
                        className="login-input"
                        type="email"
                        placeholder="Email"
                        value={email}
                        onChange={(e) =>
                            setEmail(e.target.value)
                        }
                    />

                    <input
                        className="login-input"
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) =>
                            setPassword(e.target.value)
                        }
                    />

                    <button
                        className="login-btn"
                        type="submit"
                    >
                        Login
                    </button>

                </form>

                <p className="register-text">
                    Bạn chưa có tài khoản?
                </p>

                <div className="action-buttons">

                    <Link to="/register">
                        <button
                            className="register-btn"
                            type="button"
                        >
                            Register
                        </button>
                    </Link>

                    <Link to="/forgot-password">
                        <button
                            className="forgot-btn"
                            type="button"
                        >
                            Forgot Password
                        </button>
                    </Link>

                </div>

            </div>

        </div>
    );
}