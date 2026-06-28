import axiosClient from "./axiosClient";

//======================
// GET
//======================

export const getAllCategories = () =>
    axiosClient.get("/categories");

export const getCategoryById = (
    id: number
) =>
    axiosClient.get(
        `/categories/${id}`
    );

export const searchCategory = (
    keyword: string
) =>
    axiosClient.get(
        `/categories/search?keyword=${keyword}`
    );

export const getDishesByCategory = (
    id: number
) =>
    axiosClient.get(
        `/categories/${id}/dishes`
    );

//======================
// POST
//======================

export const createCategory = (
    data: any
) =>
    axiosClient.post(
        "/categories",
        data
    );

//======================
// PUT
//======================

export const updateCategory = (
    id: number,
    data: any
) =>
    axiosClient.put(
        `/categories/${id}`,
        data
    );

//======================
// DELETE
//======================

export const deleteCategory = (
    id: number
) =>
    axiosClient.delete(
        `/categories/${id}`
    );