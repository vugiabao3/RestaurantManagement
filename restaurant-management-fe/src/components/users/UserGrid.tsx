import type { User } from "../../types/user";
import UserCard from "./UserCard";

interface Props {
    users: User[];
    reload: () => void;
}

export default function UserGrid({ users, reload }: Props) {
    return (
        <div className="user-grid">
            {users.map((u) => (
                <UserCard
                    key={u.id}
                    user={u}
                    onUpdated={reload}
                />
            ))}
        </div>
    );
}