export interface User {
    id: number;
    fullName: string;
    username: string;
    role: string;
}

export interface ChangeRoleRequest {
    userId: number;
    role: string;
}

