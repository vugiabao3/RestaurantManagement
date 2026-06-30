import { useEffect, useState } from "react";
import "../../styles/menus/menuPage.css";

import {
    getAllMenus,
    getCategoriesByMenu,
    deleteMenu
} from "../../api/menuApi";
import { useNavigate } from "react-router-dom";
import MenuCategoryList from "../../components/menus/MenuCategoryList";
import MenuCard from "../../components/menus/MenuCard";
import MenuModal from "../../components/menus/MenuModal";
import { getDishesByCategory } from "../../api/categoryApi";
import MenuForm from "../../components/menus/MenuForm";
import { canManage } from "../../utils/auth";
import { addToCart } from "../../api/orderApi";
import { getCart } from "../../api/orderApi";
export default function MenuPage() {
const navigate = useNavigate();
    const [menus, setMenus] = useState<any[]>([]);
    const [categories, setCategories] = useState<any[]>([]);
    const [dishes, setDishes] = useState<any[]>([]);

    const [selectedMenu, setSelectedMenu] = useState<any>(null);
    const [selectedCategory, setSelectedCategory] = useState<any>(null);

    const [open, setOpen] = useState(false);
    const [editing, setEditing] = useState<any>(null);

    const [cartCount, setCartCount] = useState(0);

    // MODAL ADD TO CART
    const [openCart, setOpenCart] = useState(false);
    const [selectedDish, setSelectedDish] = useState<any>(null);
    const [quantity, setQuantity] = useState(1);
    const [tableId, setTableId] = useState(1);

    useEffect(() => {
        loadMenus();
            loadCart();   // ❗ THÊM

    }, []);

    const loadMenus = async () => {
        const res = await getAllMenus();
        setMenus(res.data);
    };

    // MENU CLICK
    const handleSelectMenu = async (menu: any) => {
        setSelectedMenu(menu);
        const res = await getCategoriesByMenu(menu.menuId);
        setCategories(res.data);
    };
const loadCart = async () => {
    try {
        const res = await getCart();

        const total = (res.data || []).reduce(
            (sum: number, item: any) => sum + item.quantity,
            0
        );

        setCartCount(total);

    } catch (err) {
        console.log("cart error", err);
    }
};
    // CATEGORY CLICK
    const handleSelectCategory = async (category: any) => {
        setSelectedCategory(category);
        const res = await getDishesByCategory(category.categoryId);
        setDishes(res.data);
    };

    // OPEN MODAL ADD TO CART
    const openAddToCart = (dish: any) => {
        setSelectedDish(dish);
        setQuantity(1);
        setTableId(1);
        setOpenCart(true);
    };

    // CONFIRM ADD TO CART
    const handleConfirmAddToCart = async () => {

    try {

        const userIdRaw = localStorage.getItem("userId");
        const memberIdRaw = localStorage.getItem("memberId");

        // ❗ CHECK ĐÚNG
        if (!userIdRaw) {
            alert("Không tìm thấy userId. Vui lòng đăng nhập lại!");
            return;
        }

        if (!selectedDish) {
            alert("Chưa chọn món ăn");
            return;
        }

        const payload = {
            customerId: Number(userIdRaw),   // backend vẫn cần field này
            dishId: selectedDish.dishId,
            quantity: quantity,
            tableId: tableId,
            memberId: memberIdRaw ? Number(memberIdRaw) : 0
        };

        console.log("SEND CART:", payload);

        await addToCart(payload);

        await loadCart();   // ❗ QUAN TRỌNG
        alert("Thêm vào giỏ thành công");

        setOpenCart(false);

    } catch (err: any) {

        console.log(err?.response?.data || err);

        alert("Thêm vào giỏ thất bại");

    }
};
    // MENU CRUD
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
                 {/* 🛒 CART BUTTON */}
    <button
        className="cart-btn"
        onClick={() => navigate("/cart")}
    >
        🛒 Giỏ hàng ({cartCount})
    </button>
 

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

            {/* MENU LIST */}
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

            {/* CATEGORY */}
            {selectedMenu && (
                <div className="menu-detail">
                    <h2>Category của: {selectedMenu.name}</h2>

                    <MenuCategoryList
                        categories={categories}
                        onSelectCategory={handleSelectCategory}
                    />
                </div>
            )}

            {/* DISH LIST */}
            {selectedCategory && (
                <div className="dish-section">

                    <h2>Món ăn trong: {selectedCategory.name}</h2>

                    <div className="dish-grid">

                        {dishes.map((d: any) => (
                            <div key={d.dishId} className="dish-card">

                                <div className="dish-image">
                                    <img src={d.imageUrl} alt={d.name} />
                                </div>

                                <div className="dish-content">

                                    <h3>{d.name}</h3>
                                    <p>{d.description}</p>

                                    <div className="dish-footer">

                                        <span className="dish-price">
                                            {d.price.toLocaleString()} đ
                                        </span>

                                        <button
                                            className="add-cart-btn"
                                            onClick={() => openAddToCart(d)}
                                        >
                                            ➕ Thêm vào giỏ
                                        </button>

                                    </div>

                                </div>

                            </div>
                        ))}

                    </div>

                </div>
            )}

            {/* ADD TO CART MODAL */}
            {openCart && selectedDish && (
                <div className="cart-modal-overlay">

                    <div className="cart-modal">

                        <h2>Thêm vào giỏ</h2>

                        <p><b>{selectedDish.name}</b></p>

                        <label>Số lượng</label>
                        <input
                            type="number"
                            min={1}
                            value={quantity}
                            onChange={(e) => setQuantity(Number(e.target.value))}
                        />

                        <label>Bàn</label>
                        <input
                            type="number"
                            value={tableId}
                            onChange={(e) => setTableId(Number(e.target.value))}
                        />

                        <div className="cart-actions">

                            <button onClick={() => setOpenCart(false)}>
                                Hủy
                            </button>

                            <button onClick={handleConfirmAddToCart}>
                                Xác nhận
                            </button>

                        </div>

                    </div>

                </div>
            )}

            {/* MENU MODAL */}
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

        </div>
    );
}
