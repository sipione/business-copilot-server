"use server";
import { cookies } from "next/headers";
import { redirect } from "next/navigation";

export const getUserCredentials = () => {
    const accessToken = cookies().get('accessToken');
    const userId = cookies().get('userId');
    return accessToken && userId ? { accessToken, userId } : null;
};

export const logout = () => {
    const cookieStore = cookies();
    cookieStore.delete('accessToken', { path: '/' });
    cookieStore.delete('userId', { path: '/' });
    redirect('/login');
};

export const setUserCredentials = (accessToken, userId) => {
    const cookieStore = cookies();
    cookieStore.set('accessToken', accessToken, { expires: new Date(Date.now() + 86400e3), path: '/' }); // Expires in 1 day
    cookieStore.set('userId', userId, { expires: new Date(Date.now() + 86400e3), path: '/' }); // Expires in 1 day
};