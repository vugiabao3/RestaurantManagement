import { useEffect, useState } from "react";

import "../../styles/users/changeRoleModal.css";

import userApi from "../../api/userApi";

import type { User } from "../../types/user";

interface Props {

    open: boolean;

    user: User | null;

    onClose: () => void;

    onSuccess: () => void;

}

const roles = [

    "Admin",

    "Cashier",

    "Chef",

    "Customer"

];

export default function ChangeRoleModal({

    open,

    user,

    onClose,

    onSuccess

}: Props) {

    const [role, setRole] = useState("");

    const [loading, setLoading] = useState(false);

    useEffect(() => {

        if (user) {

            setRole(user.role);

        }

    }, [user]);

    if (!open || !user)

        return null;

    const handleSave = async () => {

        try {

            setLoading(true);

            await userApi.updateRole({

                userId: user.userId,

                role

            });

            alert("Cập nhật Role thành công.");

            onSuccess();

            onClose();

        }

        catch (err) {

            console.log(err);

            alert("Đổi Role thất bại.");

        }

        finally {

            setLoading(false);

        }

    };

    return (

        <div className="modal-overlay">

            <div className="role-modal">

                <h2>

                    Đổi Role

                </h2>

                <hr />

                <div className="modal-user">

                    <p>

                        <b>ID:</b>

                        {user.userId}

                    </p>

                    <p>

                        <b>Họ tên:</b>

                        {user.fullName}

                    </p>

                    <p>

                        <b>Username:</b>

                        {user.username}

                    </p>

                </div>

                <label>

                    Role

                </label>

                <select

                    value={role}

                    onChange={(e) =>

                        setRole(e.target.value)

                    }

                >

                    {

                        roles.map(r => (

                            <option

                                key={r}

                                value={r}

                            >

                                {r}

                            </option>

                        ))

                    }

                </select>

                <div className="modal-actions">

                    <button

                        className="cancel-btn"

                        onClick={onClose}

                    >

                        Hủy

                    </button>

                    <button

                        className="save-btn"

                        disabled={loading}

                        onClick={handleSave}

                    >

                        {

                            loading

                            ?

                            "Đang lưu..."

                            :

                            "Lưu"

                        }

                    </button>

                </div>

            </div>

        </div>

    );

}