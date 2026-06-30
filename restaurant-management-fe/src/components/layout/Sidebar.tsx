import { Link } from "react-router-dom";
import "../../styles/layout/sidebar.css";

export default function Sidebar() {
const role = localStorage.getItem("role");
  return (

<div className="sidebar">

<h2>Restaurant</h2>

{/* ADMIN */}

{role === "Admin" && (

<>

<Link to="/dashboard">
Dashboard
</Link>
<Link to="/users">
        Users
    </Link>

<Link to="/foods">
Foods
</Link>

<Link to="/categories">
Categories
</Link>

<Link to="/menus">
Menus
</Link>

<Link to="/reports">
Reports
</Link>

</>

)}

{/* CHEF */}

{role === "Chef" && (

<>

<Link to="/chef">
Chef Orders
</Link>

</>

)}

{/* CASHIER */}

{role === "Cashier" && (

<>

<Link to="/members">
Member Registration
</Link>

<Link to="/payments">
Payments
</Link>

</>

)}

{/* CUSTOMER */}

{role === "Customer" && (

<>

<Link to="/menus">
Menus
</Link>

<Link to="/cart">
Cart
</Link>

</>

)}

</div>

);}