import DishForm
from "./DishForm";
import "../../styles/foods/dishModal.css";
export default function DishModal(
{
    open,
    onClose,
    onSubmit,
    initialData
}:any
){

if(!open)
return null;

return(

<div
className="modal-overlay"
>

<div
className="modal-content"
>

<h2>

Món ăn

</h2>

<DishForm
onSubmit={onSubmit}
initialData={
initialData
}
/>

<button
onClick={onClose}
>

Close

</button>

</div>

</div>

);

}