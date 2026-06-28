// Lấy Role hiện tại
export const getRole = (): string => {

    return localStorage.getItem("role") || "";

};

// =========================

// Admin

export const isAdmin = (): boolean => {

    return getRole() === "Admin";

};

// =========================

// Staff

export const isStaff = (): boolean => {

    return getRole() === "Staff";

};

// =========================

// User

export const isUser = (): boolean => {

    return getRole() === "User";

};

// =========================

// Admin hoặc Staff

export const canManage = (): boolean => {

    const role = getRole();

    return role === "Admin" || role === "Staff";

};

// =========================

// Đã đăng nhập hay chưa

export const isLoggedIn = (): boolean => {

    return !!localStorage.getItem("token");

};