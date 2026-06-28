import { useEffect, useState } from "react";
import "../../styles/menus/menuPage.css";

import {
    getAllMenus,
    getCategoriesByMenu,
    deleteMenu
} from "../../api/menuApi";
import MenuCategoryList from "../../components/menus/MenuCategoryList";
import MenuCard from "../../components/menus/MenuCard";
import MenuModal from "../../components/menus/MenuModal";
import { getDishesByCategory } from "../../api/categoryApi";
import MenuForm from "../../components/menus/MenuForm";
import { canManage } from "../../utils/auth";

export default function MenuPage() {

    const [menus, setMenus] = useState<any[]>([]);
    const [categories, setCategories] = useState<any[]>([]);
    const [selectedMenu, setSelectedMenu] = useState<any>(null);

    const [open, setOpen] = useState(false);
    const [editing, setEditing] = useState<any>(null);
    

    useEffect(() => {
        loadMenus();
    }, []);

    
    const loadMenus = async () => {
        const res = await getAllMenus();
        setMenus(res.data);
    };

    const [selectedCategory, setSelectedCategory] = useState<any>(null);
const [dishes, setDishes] = useState<any[]>([]);
    const handleSelectMenu = async (menu: any) => {
        setSelectedMenu(menu);
        const res = await getCategoriesByMenu(menu.menuId);
        setCategories(res.data);
    };
    const handleSelectCategory = async (category: any) => {

    setSelectedCategory(category);

    const res = await getDishesByCategory(category.categoryId);

    setDishes(res.data);
};

    const handleEdit = (menu: any) => {
        setEditing(menu);
        setOpen(true);
    };

    const handleDelete = async (id: number) => {
        const ok = window.confirm("Bạn chắc chắn muốn xóa menu này?");
        if (!ok) return;

        await deleteMenu(id);
        alert("Xóa thành công");
        loadMenus();
    };

    return (
        <div className="menu-page">

            <div className="menu-header">
                <h1>Quản lý Menu</h1>
                {canManage() && (
                <button
                    className="add-btn"
                    onClick={() => {
                        setEditing(null);
                        setOpen(true);
                    }}
                >
                    + Thêm Menu
                </button>
                )}
            </div>

            <div className="menu-grid">
                {menus.map((menu) => (
                    <MenuCard
                        key={menu.menuId}
                        menu={menu}
                        selected={selectedMenu?.menuId === menu.menuId}
                        onClick={() => handleSelectMenu(menu)}
                        onEdit={handleEdit}
                        onDelete={handleDelete}
                    />
                ))}
            </div>

            {selectedMenu && (
                <div className="menu-detail">
                    <h2>
                        Category của: {selectedMenu.name}
                    </h2>

                    <MenuCategoryList
    categories={categories}
    onSelectCategory={handleSelectCategory}
/>
                </div>
            )}

            <MenuModal open={open} onClose={() => setOpen(false)}>
                <MenuForm
                    menu={editing}
                    onSuccess={() => {
                        setOpen(false);
                        setEditing(null);
                        loadMenus();
                    }}
                />
            </MenuModal>
            {selectedCategory && (
    <div className="dish-section">

        <h2>
            Món ăn trong: {selectedCategory.name}
        </h2>

        <div className="dish-grid">

            {dishes.map((d: any) => (
                <div className="dish-card">

    <div className="dish-image">

        <img
            src={d.imageUrl}
            alt={d.name}
        />

    </div>

    <div className="dish-content">

        <h3>
            {d.name}
        </h3>

        <p>
            {d.description}
        </p>

        <div className="dish-footer">

            <span className="dish-price">

                {d.price.toLocaleString()} đ

            </span>

        </div>

    </div>

</div>
            ))}

        </div>

    </div>
)}

        </div>
        
    );
}