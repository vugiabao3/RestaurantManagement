import "../../styles/orders/cart.css";

interface Props {
    item: any;
    onIncrease: (item: any) => void;
    onDecrease: (item: any) => void;
    onDelete: (id: number) => void;
}

export default function CartItem({
    item,
    onIncrease,
    onDecrease,
    onDelete
}: Props) {

    return (
        <div className="cart-item">

            <img src={item.imageUrl} />

            <div className="cart-info">
                <h4>{item.dishName}</h4>
                <p>{item.price?.toLocaleString()} đ</p>
            </div>

            <div className="cart-actions">

                <button onClick={() => onDecrease(item)}>-</button>

                <span>{item.quantity}</span>

                <button onClick={() => onIncrease(item)}>+</button>

                <button
                    className="delete"
                    onClick={() => onDelete(item.orderItemId)}
                >
                    X
                </button>

            </div>

        </div>
    );
}