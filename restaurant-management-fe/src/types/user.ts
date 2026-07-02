export interface User {

    userId: number;

    username: string;

    fullName: string;

    email: string;

    phone: string;

    role: string;

    createdAt: string;

    lastLogin?: string | null;

    status: boolean;

}