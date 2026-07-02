import axiosClient from "./axiosClient";

export const getAvailableTables = () => {
    return axiosClient.get("/Tables/available");
};