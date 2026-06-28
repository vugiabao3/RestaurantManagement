import DishCard
from "../foods/DishCard";
import "../../styles/categories/category.css";
interface Props {
    dishes: any[];
}

export default function CategoryDishList({
    dishes
}: Props) {

    return (

        <div className="dish-grid">

            {
                dishes.map(dish => (

                    <DishCard
                        key={dish.dishId}
                        dish={dish}
                        
                    />

                ))
            }

        </div>

    );
}