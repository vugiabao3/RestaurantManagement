import axiosClient from "./axiosClient";

export const addToCart = (data: any) =>
    axiosClient.post("/Orders/cart/items", data);

export const getCart = (customerId: number) =>
    axiosClient.get(`/Orders/cart/${customerId}`);

export const updateCartItem = (data: any) =>
    axiosClient.put("/Orders/cart/items", data);

export const deleteCartItem = (id: number) =>
    axiosClient.delete(`/Orders/cart/items/${id}`);

export const clearCart = (customerId: number) =>
    axiosClient.delete(`/Orders/cart/${customerId}`);

export const placeOrder = (customerId: number) =>
    axiosClient.post("/Orders/place", { customerId });

export const getPendingOrders = () =>
    axiosClient.get("/Orders/pending");
export const updateOrderStatus = (data: {
    orderId: number;
    status: string;
}) =>
    axiosClient.put("/Orders/status", data);
    // LẤY TẤT CẢ ORDER CHO CHEF
export const getChefOrders = () => {
    return axiosClient.get("/Orders/pending"); 
};