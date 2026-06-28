export interface LoginRequest {
    email: string;
    password: string;
}
export interface RegisterRequest {
    username: string;
    password: string;
    fullName: string;
    email: string;
    phone: string;
}
export interface ForgotPasswordRequest {
    email: string;
}


export interface ResetPasswordRequest {
    email: string;
    otp: string;
    newPassword: string;
}
