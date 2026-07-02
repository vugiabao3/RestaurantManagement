import axiosClient from "./axiosClient";

import type { User } from "../types/user";

interface UpdateRoleRequest {

    userId: number;

    role: string;

}

const BASE_URL = "/users";

const userApi = {

    //--------------------------------------
    // GET ALL USERS
    //--------------------------------------
    async getAll(): Promise<User[]> {

        const res = await axiosClient.get(
            `${BASE_URL}`
        );

        return res.data;

    },

    //--------------------------------------
    // UPDATE ROLE
    //--------------------------------------
    async updateRole(data: UpdateRoleRequest) {

        const res = await axiosClient.put(
            `${BASE_URL}/role`,
            data
        );

        return res.data;

    }

};

export default userApi;