import {
    Routes,
    Route
}
from "react-router-dom";

import Login from "../pages/Auth/Login";
import Register from "../pages/Auth/Register";
import ForgotPassword from "../pages/Auth/ForgotPassword";
import ResetPassword from "../pages/Auth/ResetPassword";

import Dashboard from "../pages/Dashboard/Dashboard";
import FoodPage from "../pages/Foods/FoodPage";
import CategoryPage from "../pages/Categories/CategoryPage";
import MenuPage from "../pages/Menus/MenuPage";
import ReportPage from "../pages/Reports/ReportPage";

import MainLayout from "../layouts/MainLayout";
import ProtectedRoute from "./ProtectedRoute";

export default function AppRoutes() {

    return (

        

            <Routes>

                <Route
                    path="/"
                    element={<Login />}
                />

                <Route
                    path="/register"
                    element={<Register />}
                />

                <Route
                    path="/forgot-password"
                    element={<ForgotPassword />}
                />

                <Route
                    path="/reset-password"
                    element={<ResetPassword />}
                />

                <Route
                    element={
                        <ProtectedRoute>

                            <MainLayout />

                        </ProtectedRoute>
                    }
                >

                    <Route
                        path="/dashboard"
                        element={
                            <Dashboard />
                        }
                    />

                    <Route
                        path="/foods"
                        element={
                            <FoodPage />
                        }
                    />

                    <Route
                        path="/categories"
                        element={
                            <CategoryPage />
                        }
                    />

                    <Route
                        path="/menus"
                        element={
                            <MenuPage />
                        }
                    />

                    <Route
                        path="/reports"
                        element={
                            <ReportPage />
                        }
                    />

                </Route>

            </Routes>

    );
}