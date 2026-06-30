import "../../styles/foods/dish.css";
import type { Dish } from "../../types/dish";

import { canManage } from "../../utils/auth";

interface Props {
    dish: Dish;
    onEdit?: (dish: Dish) => void;

    onDelete?: (id:number) => void;

}

export default function DishCard({
    dish,
    onEdit,
    onDelete,
    
}: Props) {

    return (

        <div className="dish-card">

            <div className="dish-image">

                <img
                    src={dish.imageUrl}
                    alt={dish.name}
                    onError={(e) => {
                        e.currentTarget.src =
                            "https://placehold.co/600x400?text=Food";
                    }}
                />

            </div>

            <div className="dish-content">

                <h3>
                    {dish.name}
                </h3>

                <p>
                    {dish.description}
                </p>

                <div className="dish-footer">

                    <span className="price">
                        {dish.price.toLocaleString()} đ
                    </span>

                    <span
                        className={
                            dish.status
                                ? "status active"
                                : "status inactive"
                        }
                    >
                        {
                            dish.status
                                ? "Đang bán"
                                : "Ngừng bán"
                        }
                    </span>

                </div>

                {(onEdit || onDelete) && canManage() && (
    <div className="dish-actions">

        {onEdit && (
            <button
                className="edit-btn"
                onClick={() => onEdit(dish)}
            >
                ✏️ Sửa
            </button>
        )}

        {onDelete && (
            <button
                className="delete-btn"
                onClick={() => onDelete(dish.dishId)}
            >
                🗑 Xóa
            </button>
        )}

        

    </div>
    
)}

            </div>

        </div>

    );
}