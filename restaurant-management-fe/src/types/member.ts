export interface RegisterMemberRequest {

    fullName: string;

    phoneNumber: string;

    userId: number;

}

export interface RegisterMemberResponse {

    memberId: number;

    cardId: string;

    message: string;

}