import axios from './api';

export async function getWorkoutDays() {
    const res = await axios.get('/workoutDay');
    return res.data;
}

// Add getById, create, update, delete
