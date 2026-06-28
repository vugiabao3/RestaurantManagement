import LoginForm from "../../components/auth/LoginForm";
import "../../styles/auth/login.css";

export default function Login() {
    return (
        <div>
            <h1 className="h1-login">Login</h1>

            <LoginForm />
        </div>
    );
}