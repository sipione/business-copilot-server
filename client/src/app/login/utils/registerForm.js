"use client"
import { Button, TextField } from "@mui/material";
import { useState } from "react";
import styles from "../../../styles/login/index.module.css";

const RegisterFormComponent = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(email, password, confirmPassword);
    }

    return (
        <form className={`${styles.RegisterFormContainer}`} onSubmit={handleSubmit}>
            <TextField
                id="outlined-password-input"
                label="E-mail"
                type="email"
                autoComplete="current-password"
                value={email}
                onChange={e => setEmail(e.target.value)}
            />
            <TextField
                id="outlined-password-input"
                label="Password"
                type="password"
                autoComplete="current-password"
                value={password} 
                onChange={e => setPassword(e.target.value)}
            />
            <TextField
                id="outlined-password-input"
                label="Confirm Password"
                type="password"
                autoComplete="current-password"
                value={confirmPassword} 
                onChange={e => setConfirmPassword(e.target.value)}
            />
            <Button variant="contained" type="submit">Register</Button>
        </form>
    );
}

export default RegisterFormComponent;