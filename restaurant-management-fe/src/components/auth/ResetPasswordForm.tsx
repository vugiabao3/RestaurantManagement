import { useState } from "react";

import {
    resetPassword
}
from "../../api/authApi";

import {
    useLocation
}
from "react-router-dom";

import "../../styles/auth/resetPassword.css";

export default function ResetPasswordForm() {

    const location =
        useLocation();

    const emailFromForgot =
        location.state?.email ?? "";

    const [email, setEmail] =
        useState(emailFromForgot);

    const [otp, setOtp] =
        useState("");

    const [newPassword,
        setNewPassword] =
        useState("");

    const handleSubmit = async (
        e: React.FormEvent
    ) => {

        e.preventDefault();

        try {

            await resetPassword({
                email,
                otp,
                newPassword
            });

            alert(
                "Đổi mật khẩu thành công"
            );

        } catch (error) {

            console.error(error);

            alert(
                "Đổi mật khẩu thất bại"
            );
        }
    };

    return (
        <div className="reset-page">

            <div className="reset-container">

                <h2 className="reset-title">
                    Reset Password
                </h2>

                <p className="reset-subtitle">
                    Nhập OTP đã gửi về email
                </p>

                <form
                    className="reset-form"
                    onSubmit={handleSubmit}
                >

                    <input
                        className="reset-input"
                        placeholder="Email"
                        value={email}
                        onChange={(e) =>
                            setEmail(
                                e.target.value
                            )
                        }
                    />

                    <input
                        className="reset-input"
                        placeholder="OTP"
                        value={otp}
                        onChange={(e) =>
                            setOtp(
                                e.target.value
                            )
                        }
                    />

                    <input
                        className="reset-input"
                        type="password"
                        placeholder="New Password"
                        value={newPassword}
                        onChange={(e) =>
                            setNewPassword(
                                e.target.value
                            )
                        }
                    />

                    <button
                        className="reset-btn"
                        type="submit"
                    >
                        Reset Password
                    </button>

                </form>

            </div>

        </div>
    );
}