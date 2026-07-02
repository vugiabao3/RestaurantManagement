import { useEffect, useState } from "react";

import userApi from "../../api/userApi";

import type { User } from "../../types/user";

import UserGrid from "../../components/users/UserGrid";

import "../../styles/users/userPage.css";

export default function UserPage() {

    const [users, setUsers] =

        useState<User[]>([]);

    const [loading, setLoading] =

        useState(false);

    const fetchUsers = async () => {

        try {

            setLoading(true);

            const data = await userApi.getAll();

            setUsers(data);

        }

        catch (err) {

            console.log(err);

        }

        finally {

            setLoading(false);

        }

    };

    useEffect(() => {

        fetchUsers();

    }, []);

    return (

        <div className="user-page">

            <div className="user-page-header">

                <div>

                    <h1>

                        👥 Quản lý người dùng

                    </h1>

                    <p>

                        Quản lý quyền hệ thống

                    </p>

                </div>

            </div>

            {

                loading

                    ?

                    <div className="loading">

                        Đang tải dữ liệu...

                    </div>

                    :

                    users.length === 0

                        ?

                        <div className="empty">

                            Không có người dùng.

                        </div>

                        :

                        <UserGrid

                            users={users}

                            reload={fetchUsers}

                        />

            }

        </div>

    );

}