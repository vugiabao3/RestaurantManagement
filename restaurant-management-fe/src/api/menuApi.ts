import axiosClient from "./axiosClient";

export const getAllMenus = () => axiosClient.get("/menus");

export const getCategoriesByMenu = (id: number) =>
    axiosClient.get(`/menus/${id}/categories`);

export const createMenu = (data: any) =>
    axiosClient.post("/menus", data);

export const updateMenu = (id: number, data: any) =>
    axiosClient.put(`/menus/${id}`, data);

export const deleteMenu = (id: number) =>
    axiosClient.delete(`/menus/${id}`);