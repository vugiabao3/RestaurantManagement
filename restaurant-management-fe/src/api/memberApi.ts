import axiosClient from "./axiosClient";

import type {
    RegisterMemberRequest,
    RegisterMemberResponse
} from "../types/member";

export const registerMember = (
    data: RegisterMemberRequest
) => {

    return axiosClient.post<RegisterMemberResponse>(
        "/members/register",
        data
    );

};