import axios from './api';

export async function registerUser(user) {
    const res = await axios.post('/user', user);
    return res.data;
}

export async function getAllUsers() {
    const res = await axios.get('/user');
    return res.data;
}

export async function getUserById(id) {
    const res = await axios.get(`/user/${id}`);
    return res.data;
}

export async function updateUser(id, user) {
    const res = await axios.put(`/user/${id}`, user);
    return res.data;
}

export async function deleteUser(id) {
    const res = await axios.delete(`/user/${id}`);
    return res.data;
}
