import "../../styles/users/userCard.css";

import type { User } from "../../types/user";

interface Props {

    user: User;

    onChangeRole: (user: User) => void;

}

export default function UserCard({

    user,

    onChangeRole

}: Props) {

    const roleClass = user.role.toLowerCase();

    return (

        <div className="user-card">

            <div className="user-card-header">

                <h3>{user.fullName}</h3>

                <span className={`role-badge ${roleClass}`}>

                    {user.role}

                </span>

            </div>

            <div className="user-info">

                <p>

                    <b>ID:</b>

                    {user.userId}

                </p>

                <p>

                    <b>Username:</b>

                    {user.username}

                </p>

                <p>

                    <b>Email:</b>

                    {user.email}

                </p>

                <p>

                    <b>Phone:</b>

                    {user.phone}

                </p>

                <p>

                    <b>Trạng thái:</b>

                    {

                        user.status

                        ?

                        "Đang hoạt động"

                        :

                        "Đã khóa"

                    }

                </p>

            </div>

            <button

                className="change-role-btn"

                onClick={() => onChangeRole(user)}

            >

                Đổi Role

            </button>

        </div>

    );

}