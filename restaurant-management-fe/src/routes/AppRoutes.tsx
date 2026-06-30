import { Routes, Route } from "react-router-dom";

import Login from "../pages/Auth/Login";
import Register from "../pages/Auth/Register";
import ForgotPassword from "../pages/Auth/ForgotPassword";
import ResetPassword from "../pages/Auth/ResetPassword";

import CashierDashboard from "../pages/Cashier/CashierDashboard";
import PaymentPage from "../pages/Cashier/PaymentPage";
import Dashboard from "../pages/Dashboard/Dashboard";
import FoodPage from "../pages/Foods/FoodPage";
import CategoryPage from "../pages/Categories/CategoryPage";
import MenuPage from "../pages/Menus/MenuPage";
import ReportPage from "../pages/Reports/ReportPage";
import FoodReportPage from "../pages/Reports/FoodReportPage";
import RevenueReportPage from "../pages/Reports/RevenueReportPage";
import CartPage from "../pages/Orders/CartPage";
import ChefOrderPage from "../pages/Chef/ChefOrderPage";

import MainLayout from "../layouts/MainLayout";
import ProtectedRoute from "./ProtectedRoute";

import UserPage from "../pages/Users/UserPage";
import MemberRegisterPage from "../pages/Cashier/CashierDashboard";

export default function AppRoutes() {
    return (
        <Routes>

            {/* PUBLIC ROUTES */}
            <Route path="/" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/forgot-password" element={<ForgotPassword />} />
            <Route path="/reset-password" element={<ResetPassword />} />

            {/* PROTECTED ROUTES */}
            <Route element={<ProtectedRoute><MainLayout /></ProtectedRoute>}>

                <Route path="/dashboard" element={<Dashboard />} />
                <Route path="/foods" element={<FoodPage />} />
                <Route path="/categories" element={<CategoryPage />} />
                <Route path="/menus" element={<MenuPage />} />

                <Route path="/users" element={<UserPage />} />   {/* ✅ FIX Ở ĐÂY */}

                <Route path="/cart" element={<CartPage />} />
                <Route path="/chef" element={<ChefOrderPage />} />

                <Route path="/reports" element={<ReportPage />} />
                <Route path="/reports/food" element={<FoodReportPage />} />
                <Route path="/reports/revenue" element={<RevenueReportPage />} />

                <Route path="/cashier" element={<CashierDashboard />} />
                <Route path="/payments" element={<PaymentPage />} />

            </Route>

        </Routes>
    );
}