// This is not safe at all and this should never be done in a proper production.
// as such please dont enter any information into this app that you do not want everyone to be able to access.

import axios from './api';

export async function loginUser(email, password) {
    const res = await axios.post('/user/login', { email, password });
    return res.data;
}

// we're just saving the user ID (GUID string) and not the object
export function loginAs(userId) {
    if (!isGuid(userId)) {
        throw new Error('Invalid userId format (must be a GUID)');
    }
    localStorage.setItem('userId', userId);
}

export function getLoggedInUserId() {
    const userId = localStorage.getItem('userId');
    if (!userId || !isGuid(userId)) return null;
    return userId;
}

export function logout() {
    localStorage.removeItem('userId');
}
// Super simple anti tamper, we're just checking if its a valid GUID
export function isGuid(str) {
    return /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/.test(str);
}