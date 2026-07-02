import { useState } from "react";

import type { User } from "../../types/user";

import UserCard from "./UserCard";

import ChangeRoleModal from "./ChangeRoleModal";

import "../../styles/users/userGrid.css";

interface Props {

    users: User[];

    reload: () => void;

}

export default function UserGrid({

    users,

    reload

}: Props) {

    //------------------------------------------
    // Modal State
    //------------------------------------------

    const [openModal, setOpenModal] =

        useState(false);

    const [selectedUser, setSelectedUser] =

        useState<User | null>(null);

    //------------------------------------------
    // Open Modal
    //------------------------------------------

    const handleOpenModal = (

        user: User

    ) => {

        setSelectedUser(user);

        setOpenModal(true);

    };

    //------------------------------------------
    // Close Modal
    //------------------------------------------

    const handleCloseModal = () => {

        setOpenModal(false);

        setSelectedUser(null);

    };

    //------------------------------------------
    // Render
    //------------------------------------------

    return (

        <>

            <div className="user-grid">

                {

                    users.map(user => (

                        <UserCard

                            key={user.userId}

                            user={user}

                            onChangeRole={handleOpenModal}

                        />

                    ))

                }

            </div>

            <ChangeRoleModal

                open={openModal}

                user={selectedUser}

                onClose={handleCloseModal}

                onSuccess={reload}

            />

        </>

    );

}