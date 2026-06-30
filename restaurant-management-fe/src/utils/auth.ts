// Lấy Role hiện tại
export const getRole = (): string => {

    return localStorage.getItem("role") || "";

};

// =========================

// Admin

export const isAdmin = (): boolean => {

    return getRole() === "Admin";

};
export const isChef = (): boolean => {
    return getRole() === "Chef";
};
// =========================

// Staff

export const isStaff = (): boolean => {

    return getRole() === "Staff";

};

// =========================

// Customer

export const isCustomer = (): boolean => {

    return getRole() === "Customer";

};

// =========================

// Admin hoặc Chef

export const canManage = (): boolean => {

    const role = getRole();

    return role === "Admin" || role === "Chef";

};

// =========================

// Đã đăng nhập hay chưa

export const isLoggedIn = (): boolean => {

    return !!localStorage.getItem("token");

};