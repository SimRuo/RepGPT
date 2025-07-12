import axios from './api';

export async function getExercises() {
    const res = await axios.get('/exercise');
    return res.data;
}
