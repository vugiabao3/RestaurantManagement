import { useEffect, useState } from "react";

import "../../styles/categories/categoryPage.css";
import { canManage } from "../../utils/auth";
import {

    getAllCategories,

    getDishesByCategory,

    createCategory,

    updateCategory,

    deleteCategory,

    searchCategory

} from "../../api/categoryApi";

import CategoryCard
from "../../components/categories/CategoryCard";

import CategoryDishList
from "../../components/categories/CategoryDishList";

import CategoryForm
from "../../components/categories/CategoryForm";

import CategoryModal
from "../../components/categories/CategoryModal";

import SearchCategory
from "../../components/categories/SearchCategory";

export default function CategoryPage() {

    const [categories,
        setCategories] = useState<any[]>([]);

    const [dishes,
        setDishes] = useState<any[]>([]);

    const [selectedCategory,
        setSelectedCategory] = useState<any>(null);

    const [keyword,
        setKeyword] = useState("");

    const [open,
        setOpen] = useState(false);

    const [editingCategory,
        setEditingCategory] = useState<any>(null);

    useEffect(() => {

        loadCategories();

    }, []);

    //---------------------------------

    const loadCategories =
        async () => {

            const res =
                await getAllCategories();

            setCategories(
                res.data
            );

        };

    //---------------------------------

    const handleSelectCategory =
        async (category: any) => {

            setSelectedCategory(
                category
            );

            const res =
                await getDishesByCategory(
                    category.categoryId
                );

            setDishes(
                res.data
            );

        };

    //---------------------------------

    const handleSearch =
        async () => {

            if (keyword.trim() === "") {

                loadCategories();

                return;

            }

            const res =
                await searchCategory(
                    keyword
                );

            setCategories(
                res.data
            );

        };

    //---------------------------------

    const handleSubmit =
async(data:any)=>{

    try{

        if(editingCategory){

            await updateCategory(

                editingCategory.categoryId,

                data

            );

            alert(
                "Cập nhật danh mục thành công!"
            );

        }

        else{

            await createCategory(
                data
            );

            alert(
                "Thêm danh mục thành công!"
            );

        }

        setOpen(false);

        setEditingCategory(null);

        loadCategories();

    }
    catch{

        alert(
            "Có lỗi xảy ra!"
        );

    }

};

    //---------------------------------

    const handleEdit =
        (category: any) => {

            setEditingCategory(
                category
            );

            setOpen(true);

        };

    //---------------------------------

    const handleDelete =
async(id:number)=>{

    if(
        !window.confirm(
            "Bạn chắc chắn muốn xóa?"
        )
    )
        return;

    try{

        await deleteCategory(id);

        alert(
            "Xóa danh mục thành công!"
        );

        if(
            selectedCategory?.categoryId===id
        ){

            setSelectedCategory(null);

            setDishes([]);

        }

        loadCategories();

    }
    catch{

        alert(
            "Không thể xóa danh mục!"
        );

    }

};

    //---------------------------------

    return (

        <div className="category-page">

            <h1>

                Quản lý danh mục

            </h1>

            <SearchCategory

                keyword={keyword}

                setKeyword={setKeyword}

                onSearch={handleSearch}

                onAdd={() => {

                    setEditingCategory(null);

                    setOpen(true);

                }}

            />

            <div className="category-grid">

                {

                    categories.map((category) => (

                        <div
                            key={category.categoryId}
                            className="category-item"
                        >

                            <CategoryCard

                                category={category}

                                selected={
                                    selectedCategory?.categoryId ===
                                    category.categoryId
                                }

                                onClick={() =>
                                    handleSelectCategory(category)
                                }

                            />
                            {canManage() && (
                            <div
                                className="category-action"
                            >

                                <button

                                    className="edit-btn"

                                    onClick={() =>
                                        handleEdit(category)
                                    }

                                >

                                    Edit

                                </button>

                                <button

                                    className="delete-btn"

                                    onClick={() =>
                                        handleDelete(
                                            category.categoryId
                                        )
                                    }

                                >

                                    Delete

                                </button>

                            </div>
)}
                        </div>

                    ))

                }

            </div>

            {

                selectedCategory ? (

                    <div
                        className="category-dishes-wrapper"
                    >

                        <h2>

                            Danh sách món ăn -

                            <span
                                className="category-name"
                            >

                                {" "}

                                {selectedCategory.name}

                            </span>

                        </h2>

                        <CategoryDishList

                            dishes={dishes}

                        />

                    </div>

                ) : (

                    <div
                        className="empty-category"
                    >

                        <h3>

                            🍽️ Chưa chọn danh mục

                        </h3>

                        <p>

                            Hãy chọn một danh mục
                            để xem món ăn.

                        </p>

                    </div>

                )

            }

            <CategoryModal

                open={open}

                title={
                    editingCategory
                        ? "Update Category"
                        : "Add Category"
                }

                onClose={() => {

                    setOpen(false);

                    setEditingCategory(null);

                }}

            >

                <CategoryForm

                    category={editingCategory}

                    onSubmit={handleSubmit}

                />

            </CategoryModal>

        </div>

    );

}