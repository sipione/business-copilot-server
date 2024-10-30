"use client";
import { Button, TextField } from "@mui/material";
import { useState } from "react";
import styles from "../../../styles/login/index.module.css";
import axios from "axios";
import { useRouter } from "next/navigation"; // Import useRouter for navigation
import { setUserCredentials } from "@/hooks/useAuthentication"; // Import the function

const LoginFormComponent = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const router = useRouter(); // Initialize useRouter

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        const postUrl = "http://localhost:5043/User/login";

        // Create the data Content-Type: multipart/form-data
        const data = new FormData();
        data.append('email', email);
        data.append('password', password);

        try {
            const response = await axios.post(postUrl, data);
            // Call the server action to save credentials
            await setUserCredentials(response.data.accessToken, response.data.userId);
            router.push('/dashboard'); // Redirect to dashboard after successful login
        } catch (err) {
            console.error(err); // Log error for debugging
        } finally {
            setLoading(false);
        }
    };

    return (
        <form className={`${styles.LoginFormContainer}`} onSubmit={handleSubmit}>
            <TextField
                id="outlined-email-input"
                label="E-mail"
                type="email"
                autoComplete="email"
                value={email}
                onChange={e => setEmail(e.target.value)}
                required
            />
            <TextField
                id="outlined-password-input"
                label="Password"
                type="password"
                autoComplete="current-password"
                value={password}
                onChange={e => setPassword(e.target.value)}
                required
            />
            <Button variant="contained" type="submit" disabled={loading}>
                {loading ? 'Logging in...' : 'Login'}
            </Button>
        </form>
    );
};

export default LoginFormComponent;