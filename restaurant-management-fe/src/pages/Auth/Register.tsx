import RegisterForm from "../../components/auth/RegisterForm";
import "../../styles/auth/register.css";

export default function Register() {
    return (
        <>
            <h1 className="h1-register">Register</h1>

            <RegisterForm />
        </>
    );
}