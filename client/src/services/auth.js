// This is not safe at all and this should never be done in a proper production.
// as such please dont enter any information into this app that you do not want everyone to be able to access.
import axios from './api';

export async function loginUser(email, password) {
    const res = await axios.post('/user/login', { email, password });
    return res.data;
}

export function loginAs(userId) {
    localStorage.setItem('userId', userId);
}

export function getLoggedInUserId() {
    return parseInt(localStorage.getItem('userId'), 10);
}

export function logout() {
    localStorage.removeItem('userId');
}