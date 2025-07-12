import axios from './api';

export async function getWorkoutLogs() {
    const res = await axios.get('/workoutLogs');
    return res.data;
}

// Add getById, create, update, delete
