import Sidebar from "../components/layout/Sidebar";
import Navbar from "../components/layout/Navbar";

import { Outlet } from "react-router-dom";

import "../styles/layout/dashboard.css";

export default function MainLayout() {

    return (

        <div className="layout">

            <Sidebar />

            <div className="main-content">

                <Navbar />

                <div className="page-content">

                    <Outlet />

                </div>

            </div>

        </div>

    );
}