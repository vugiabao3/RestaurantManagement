import { useState } from "react";
import { register } from "../../api/authApi";

import "../../styles/auth/register.css";

export default function RegisterForm() {

    const [username, setUsername] =
        useState("");

    const [password, setPassword] =
        useState("");

    const [fullName, setFullName] =
        useState("");

    const [email, setEmail] =
        useState("");

    const [phone, setPhone] =
        useState("");

    const handleSubmit = async (
        e: React.FormEvent
    ) => {

        e.preventDefault();

        try {

            await register({
                username,
                password,
                fullName,
                email,
                phone
            });

            alert("Đăng ký thành công");

        } catch (error) {

            console.error(error);

            alert("Đăng ký thất bại");
        }
    };

    return (
        <div className="register-page">

            <div className="register-container">

                <h2 className="register-title">
                    Create Account
                </h2>

                <p className="register-subtitle">
                    Restaurant Management System
                </p>

                <form
                    className="register-form"
                    onSubmit={handleSubmit}
                >

                    <input
                        className="register-input"
                        placeholder="Username"
                        value={username}
                        onChange={(e) =>
                            setUsername(
                                e.target.value
                            )
                        }
                    />

                    <input
                        className="register-input"
                        placeholder="Full Name"
                        value={fullName}
                        onChange={(e) =>
                            setFullName(
                                e.target.value
                            )
                        }
                    />

                    <input
                        className="register-input"
                        type="email"
                        placeholder="Email"
                        value={email}
                        onChange={(e) =>
                            setEmail(
                                e.target.value
                            )
                        }
                    />

                    <input
                        className="register-input"
                        placeholder="Phone Number"
                        value={phone}
                        onChange={(e) =>
                            setPhone(
                                e.target.value
                            )
                        }
                    />

                    <input
                        className="register-input"
                        type="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) =>
                            setPassword(
                                e.target.value
                            )
                        }
                    />

                    <button
                        className="register-submit-btn"
                        type="submit"
                    >
                        Register
                    </button>

                </form>

            </div>

        </div>
    );
}