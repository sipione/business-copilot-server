'use client';
import { getUserCredentials, logout } from '@/hooks/useAuthentication';
import { redirect } from 'next/navigation'
import { useEffect, useState } from 'react';


const DashboardPage = () => {
    const [ loading, setLoading ] = useState(true);
    const [ userCredentials, setUserCredentials ] = useState({});

    useEffect(() => {
        const usercredentials = getUserCredentials();
        if(!usercredentials){
            redirect('/login');
        }
        setUserCredentials(usercredentials);
        setLoading(false);
    }, []);


    if(loading){
        return (
            <div>
                <h1>Loading...</h1>
            </div>
        );
    }

    return (
        <div>
        <h1>Dashboard</h1>
        <div>
            <h2>Welcome {userCredentials.userId}</h2>
            <button onClick={() => {
                logout();
            }}>Logout</button>
        </div>
        </div>
    );
};

export default DashboardPage;