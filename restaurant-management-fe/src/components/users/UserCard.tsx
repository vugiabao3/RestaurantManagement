import { useState } from "react";
import type { User } from "../../types/user";
import userApi from "../../api/userApi";
import RoleSelect from "./RoleSelect";

interface Props {
    user: User;
    onUpdated: () => void;
}

export default function UserCard({ user, onUpdated }: Props) {

const [role, setRole] = useState(user?.role ?? "Customer");    const [loading, setLoading] = useState(false);

    const handleUpdate = async () => {
        try {
            setLoading(true);

            await userApi.changeRole({
                userId: user.id,
                role: role
            });

            alert("Update role success!");
            onUpdated();

        } catch (err) {
            console.log(err);
            alert("Update failed!");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="user-card">
            <h3>{user.fullName}</h3>
            <p>@{user.username}</p>

            <div className="role-box">
                <label>Role:</label>

                <RoleSelect value={role} onChange={setRole} />
            </div>

            <button
                onClick={handleUpdate}
                disabled={loading}
            >
                {loading ? "Updating..." : "Update"}
            </button>
        </div>
    );
}