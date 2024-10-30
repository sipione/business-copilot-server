"use client"
import LoginFormComponent from "./utils/loginForm";
import RegisterFormComponent from "./utils/registerForm";
import { Radio } from "@mui/material";
import { useState } from "react";
import styles from "../../styles/login/index.module.css";

const LoginPage = () => {
    const [isLogin, setIsLogin] = useState(true);
    return (
        <div className={styles.LoginContainer}>
            <div className={styles.formBox}>
                <div className={styles.swittchLoginRegister}>
                    <div className={styles.switchItem}>
                        <p>Login</p>
                        <Radio
                            checked={isLogin}
                            onChange={() => setIsLogin(true)}
                            value="a"
                            name="isLoginSwitcher"
                            inputProps={{ 'aria-label': 'A' }}
                        />
                    </div>
                    <div className={styles.switchItem}>
                        <p>Register</p>

                        <Radio
                            checked={!isLogin}
                            onChange={() => setIsLogin(false)}
                            value="b"
                            name="isLoginSwitcher"
                            inputProps={{ 'aria-label': 'Baaaaaa' }}
                        />
                    </div>
                </div>
                <div className={styles.formContainer}>
                    {isLogin ? <LoginFormComponent /> : <RegisterFormComponent />}
                </div>
            </div>
        </div>
    );
}

export default LoginPage;