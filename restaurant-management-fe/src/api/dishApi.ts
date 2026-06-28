import axiosClient from "./axiosClient";

export const getAllDishes =
() => axiosClient.get("/dishes");

export const getDishById =
(id:number) =>
axiosClient.get(`/dishes/${id}`);

export const searchDish =
(keyword:string) =>
axiosClient.get(
`/dishes/search?keyword=${keyword}`
);

export const createDish =
(data:any) =>
axiosClient.post(
"/dishes",
data
);

export const updateDish =
(
id:number,
data:any
) =>
axiosClient.put(
`/dishes/${id}`,
data
);

export const deleteDish =
(id:number) =>
axiosClient.delete(
`/dishes/${id}`
);

