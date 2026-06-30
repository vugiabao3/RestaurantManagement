import "../../styles/orders/menuOrder.css";

interface Props {
    dish: any;
    onAddToCart: (dish: any) => void;
}

export default function DishOrderCard({
    dish,
    onAddToCart
}: Props) {

    return (
        <div className="dish-order-card">

            <img
                src={dish.imageUrl}
                alt={dish.name}
                className="dish-order-img"
            />

            <div className="dish-order-body">

                <h3>{dish.name}</h3>

                <p>{dish.description}</p>

                <div className="price">
                    {dish.price.toLocaleString()} đ
                </div>

                <button
                    className="btn-add-cart"
                    onClick={() => onAddToCart(dish)}
                >
                    ➕ Thêm vào giỏ
                </button>

            </div>

        </div>
    );
}