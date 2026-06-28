import axiosClient from "./axiosClient";

export const login = (data: any) => {
    return axiosClient.post("/auth/login", data);
};

export const register = (data: any) => {
    return axiosClient.post("/auth/register", data);
};

export const forgotPassword = (data: any) => {
    return axiosClient.post("/auth/forgot-password", data);
};

export const resetPassword = (data: any) => {
    return axiosClient.post("/auth/reset-password", data);
};