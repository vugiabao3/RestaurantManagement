import { useEffect, useState } from "react";
import "../../styles/reports/report.css";

import { getAllDishes } from "../../api/dishApi";
import { getAllCategories } from "../../api/categoryApi";
import { getAllMenus } from "../../api/menuApi";

import StatisticCard from "../../components/reports/StatisticCard";

export default function FoodReportPage() {

    const [dishes, setDishes] = useState<any[]>([]);
    const [categories, setCategories] = useState<any[]>([]);
    const [menus, setMenus] = useState<any[]>([]);

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        const [dRes, cRes, mRes] = await Promise.all([
            getAllDishes(),
            getAllCategories(),
            getAllMenus()
        ]);

        setDishes(dRes.data);
        setCategories(cRes.data);
        setMenus(mRes.data);
    };

    const totalDish = dishes.length;
    const activeDish = dishes.filter(x => x.status).length;
    const inactiveDish = totalDish - activeDish;

    const totalCategory = categories.length;
    const activeCategory = categories.filter(x => x.status).length;
    const inactiveCategory = totalCategory - activeCategory;

    const totalMenu = menus.length;
    const activeMenu = menus.filter(x => x.status).length;
    const inactiveMenu = totalMenu - activeMenu;

    return (
        <div className="report-container">

            <h1>📊 Báo cáo món ăn</h1>

            <div className="report-section">

                <h2>🍜 Dish</h2>
                <div className="stat-grid">

                    <StatisticCard title="Tổng Dish" value={totalDish} icon="🍜" />
                    <StatisticCard title="Đang hoạt động" value={activeDish} icon="✅" />
                    <StatisticCard title="Ngừng hoạt động" value={inactiveDish} icon="❌" />

                </div>

            </div>

            <div className="report-section">

                <h2>📂 Category</h2>
                <div className="stat-grid">

                    <StatisticCard title="Tổng Category" value={totalCategory} icon="📂" />
                    <StatisticCard title="Active" value={activeCategory} icon="✅" />
                    <StatisticCard title="Inactive" value={inactiveCategory} icon="❌" />

                </div>

            </div>

            <div className="report-section">

                <h2>📋 Menu</h2>
                <div className="stat-grid">

                    <StatisticCard title="Tổng Menu" value={totalMenu} icon="📋" />
                    <StatisticCard title="Active" value={activeMenu} icon="✅" />
                    <StatisticCard title="Inactive" value={inactiveMenu} icon="❌" />

                </div>

            </div>

        </div>
    );
}