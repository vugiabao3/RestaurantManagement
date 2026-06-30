import axiosClient from "./axiosClient";
import type { ChangeRoleRequest, User } from "../types/user";
const userApi = {
    // Lấy danh sách user
    getAll: async (): Promise<User[]> => {
        return await axiosClient.get("/users");
    },

    // Đổi role user
    changeRole: async (data: ChangeRoleRequest) => {
        return await axiosClient.put("/users/role", data);
    }
};

export default userApi;