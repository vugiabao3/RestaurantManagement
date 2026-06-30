import { useEffect, useState } from "react";

import "../../styles/orders/menuOrder.css";

import {
    getAllMenus,
    getCategoriesByMenu
} from "../../api/menuApi";

import {
    getDishesByCategory
} from "../../api/categoryApi";

import {
    addToCart
} from "../../api/orderApi";

import DishOrderCard
from "../../components/orders/DishOrderCard";

import { useNavigate } from "react-router-dom";

export default function MenuOrderPage() {

    const navigate = useNavigate();

    const [menus, setMenus] =
        useState<any[]>([]);

    const [categories, setCategories] =
        useState<any[]>([]);

    const [dishes, setDishes] =
        useState<any[]>([]);

    const [selectedMenu,
        setSelectedMenu] =
        useState<any>(null);

    const [selectedCategory,
        setSelectedCategory] =
        useState<any>(null);

    const [cartCount,
        setCartCount] =
        useState(0);

    useEffect(() => {

        loadMenus();

    }, []);

    //--------------------------------

    const loadMenus = async () => {

        const res =
            await getAllMenus();

        setMenus(res.data);

    };

    //--------------------------------

    const handleMenu =
        async (menu: any) => {

            setSelectedMenu(menu);

            setSelectedCategory(null);

            setDishes([]);

            const res =
                await getCategoriesByMenu(
                    menu.menuId
                );

            setCategories(
                res.data
            );

        };

    //--------------------------------

    const handleCategory =
        async (category: any) => {

            setSelectedCategory(category);

            const res =
                await getDishesByCategory(
                    category.categoryId
                );

            setDishes(
                res.data
            );

        };

    //--------------------------------

    const handleAddToCart =
        async (
            dish: any,
            quantity: number
        ) => {

            try {

                await addToCart({

                    dishId:
                        dish.dishId,

                    quantity

                });

                alert(
                    "Đã thêm vào giỏ hàng"
                );

                setCartCount(
                    prev => prev + quantity
                );

            }

            catch {

                alert(
                    "Không thể thêm món"
                );

            }

        };

    //--------------------------------

    return (

<div className="order-page">

<h1>

🍽️ Đặt món

</h1>

{/* ================= MENU ================= */}

<h2>

Menu

</h2>

<div className="menu-order-grid">

{

menus.map(menu=>(

<div

key={menu.menuId}

className={

selectedMenu?.menuId===menu.menuId

?

"menu-order-card active"

:

"menu-order-card"

}

onClick={()=>

handleMenu(menu)

}

>

<h3>

{menu.name}

</h3>

<p>

{menu.description}

</p>

</div>

))

}

</div>

{/* ================= CATEGORY ================= */}

{

selectedMenu &&

<>

<h2>

Danh mục

</h2>

<div className="menu-category-grid">

{

categories.map(category=>(

<div

key={category.categoryId}

className={

selectedCategory?.categoryId===

category.categoryId

?

"menu-category-card active"

:

"menu-category-card"

}

onClick={()=>

handleCategory(category)

}

>

<h3>

{category.name}

</h3>

<p>

{category.description}

</p>

</div>

))

}

</div>

</>

}

{/* ================= DISH ================= */}

{

selectedCategory &&

<>

<h2>

Món ăn

</h2>

<div className="order-dish-grid">

{

dishes.length===0

?

<div className="empty-order">

Chưa có món ăn.

</div>

:

dishes.map(dish=>(

<DishOrderCard

key={dish.dishId}

dish={dish}

onAddToCart={

handleAddToCart

}

/>

))

}

</div>

</>

}

{/* ================= FLOATING CART ================= */}

{

cartCount>0 &&

<div

className="cart-floating"

>

<h3>

🛒 Giỏ hàng

</h3>

<p>

Đã chọn

<strong>

{" "}

{cartCount}

</strong>

món

</p>

<button

onClick={()=>

navigate("/cart")

}

>

Xem giỏ hàng

</button>

</div>

}

</div>

    );

}