interface Props{

    value:string;

    onChange:(v:string)=>void;

}

export default function PaymentMethodSelector({

    value,

    onChange

}:Props){

    return(

        <div className="payment-method">

            <h3>

                Phương thức thanh toán

            </h3>

            <label>

                <input

                    type="radio"

                    checked={value==="Cash"}

                    onChange={()=>onChange("Cash")}

                />

                Cash

            </label>

            <label>

                <input

                    type="radio"

                    checked={value==="Banking"}

                    onChange={()=>onChange("Banking")}

                />

                Banking

            </label>

        </div>

    );

}