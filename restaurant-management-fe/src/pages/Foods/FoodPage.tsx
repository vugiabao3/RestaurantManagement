import { useEffect, useState } from "react";

import {
    getAllDishes,
    searchDish,
    createDish,
    updateDish,
    deleteDish
} from "../../api/dishApi";
import { canManage } from "../../utils/auth";
import DishCard
from "../../components/foods/DishCard";

import DishModal
from "../../components/foods/DishModal";

import SearchDish
from "../../components/foods/SearchDish";

import "../../styles/foods/dish.css";
import "../../styles/foods/dishModal.css";
import "../../styles/foods/dishForm.css";

export default function FoodPage() {

    const [dishes, setDishes] =
        useState<any[]>([]);

    const [keyword, setKeyword] =
        useState("");

    const [open, setOpen] =
        useState(false);

    const [editingDish, setEditingDish] =
        useState<any>(null);

    useEffect(() => {

        loadData();

    }, []);

    const loadData = async () => {

        try {

            const res =
                await getAllDishes();

            setDishes(
                res.data
            );

        } catch (error) {

            console.error(error);

        }
    };

    const handleSearch =
        async () => {

            try {

                if (!keyword.trim()) {

                    loadData();

                    return;
                }

                const res =
                    await searchDish(
                        keyword
                    );

                setDishes(
                    res.data
                );

            } catch (error) {

                console.error(error);

            }
        };

    const handleCreate =
        async (data: any) => {

            try {

                await createDish(
                    data
                );

                alert(
                    "Thêm món thành công"
                );

                loadData();

                setOpen(false);

            } catch (error) {

                console.error(error);

                alert(
                    "Thêm món thất bại"
                );
            }
        };

    const handleUpdate =
        async (data: any) => {

            try {

                if (!editingDish)
                    return;

                await updateDish(
                    editingDish.dishId,
                    data
                );

                alert(
                    "Cập nhật thành công"
                );

                loadData();

                setOpen(false);

                setEditingDish(null);

            } catch (error) {

                console.error(error);

                alert(
                    "Cập nhật thất bại"
                );
            }
        };

    const handleDelete =
        async (id: number) => {

            const confirmDelete =
                window.confirm(
                    "Bạn có chắc muốn xóa món này?"
                );

            if (!confirmDelete)
                return;

            try {

                await deleteDish(id);

                alert(
                    "Xóa thành công"
                );

                loadData();

            } catch (error) {

                console.error(error);

                alert(
                    "Xóa thất bại"
                );
            }
        };

    return (

        <div className="food-page">

            <div className="food-header">

                <h1>
                    Quản lý món ăn
                </h1>

                {canManage() && (
                <button
                    className="add-btn"
                    onClick={() => {

                        setEditingDish(
                            null
                        );

                        setOpen(
                            true
                        );
                    }}
                >
                    + Thêm món ăn
                </button>)}

            </div>

            <SearchDish
                keyword={keyword}
                setKeyword={setKeyword}
                onSearch={handleSearch}
            />

            <div className="dish-grid">

                {dishes.map((dish) => (

                    <DishCard
                        key={dish.dishId}
                        dish={dish}
                        onEdit={(dish: any) => {

                            setEditingDish(
                                dish
                            );

                            setOpen(
                                true
                            );
                        }}
                        onDelete={
                            handleDelete
                        }
                    />

                ))}

            </div>

            <DishModal
                open={open}
                onClose={() =>
                    setOpen(false)
                }
                initialData={
                    editingDish
                }
                onSubmit={
                    editingDish
                        ? handleUpdate
                        : handleCreate
                }
            />

        </div>

    );
}