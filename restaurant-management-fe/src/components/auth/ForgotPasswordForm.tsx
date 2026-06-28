import { useState } from "react";

import {
    forgotPassword
}
from "../../api/authApi";

import {
    useNavigate
}
from "react-router-dom";

import "../../styles/auth/forgotPassword.css";

export default function ForgotPasswordForm() {

    const navigate =
        useNavigate();

    const [email, setEmail] =
        useState("");

    const handleSubmit = async (
        e: React.FormEvent
    ) => {

        e.preventDefault();

        try {

            await forgotPassword({
                email
            });

            alert(
                "OTP đã gửi tới email"
            );

            navigate(
                "/reset-password",
                {
                    state: {
                        email
                    }
                }
            );

        } catch (error) {

            console.error(error);

            alert(
                "Không gửi được OTP"
            );
        }
    };

    return (
        <div className="forgot-page">

            <div className="forgot-container">

                <h2 className="forgot-title">
                    Forgot Password
                </h2>

                <p className="forgot-subtitle">
                    Nhập email để nhận mã OTP
                </p>

                <form
                    className="forgot-form"
                    onSubmit={handleSubmit}
                >

                    <input
                        className="forgot-input"
                        type="email"
                        placeholder="Email"
                        value={email}
                        onChange={(e) =>
                            setEmail(
                                e.target.value
                            )
                        }
                    />

                    <button
                        className="forgot-btn"
                        type="submit"
                    >
                        Send OTP
                    </button>

                </form>

            </div>

        </div>
    );
}