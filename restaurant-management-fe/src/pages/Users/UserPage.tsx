import { useEffect, useState } from "react";
import userApi from "../../api/userApi";
import type { User } from "../../types/user";
import UserGrid from "../../components/users/UserGrid";
import "../../styles/users/userPage.css";

export default function UserPage() {
    const [users, setUsers] = useState<User[]>([]);
    const [loading, setLoading] = useState(false);

    const fetchUsers = async () => {
        try {
            setLoading(true);
            const data = await userApi.getAll();
            setUsers(data);
        } catch (err) {
            console.log(err);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchUsers();
    }, []);

    return (
        <div className="user-page">
            <h1>Users Management</h1>

            {loading ? (
                <p>Loading...</p>
            ) : (
                <UserGrid users={users} reload={fetchUsers} />
            )}
        </div>
    );
}