import "../../styles/orders/cart.css";

interface Props {

    total: number;

    onPlace: () => void;

}

export default function CartSummary({

    total,

    onPlace

}: Props) {

    return (

        <div className="cart-summary">

            <div>

                <h2>

                    Tổng tiền

                </h2>

                <h1>

                    {total.toLocaleString()} đ

                </h1>

            </div>

            <button

                className="place-order-btn"

                onClick={onPlace}

            >

                🍽 Đặt món

            </button>

        </div>

    );

}