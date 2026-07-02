interface Props{

    loading:boolean;

    onClick:()=>void;

}

export default function CheckoutButton({

    loading,

    onClick

}:Props){

    return(

        <button

            className="checkout-btn"

            onClick={onClick}

        >

            {

                loading

                ?

                "Đang thanh toán..."

                :

                "Thanh toán"

            }

        </button>

    );

}