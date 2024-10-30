import { NextResponse } from 'next/server';

export function middleware(req) {
    const accessToken = req.cookies.get('accessToken');

    if (!accessToken) {
        return NextResponse.redirect(new URL('/login', req.url));
    }
}

export const config = {
    matcher: ['/dashboard'], // Apply this middleware only on /dashboard route
};