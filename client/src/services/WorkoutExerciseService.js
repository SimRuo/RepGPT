import axios from './api';

export async function getWorkoutExercises() {
    const res = await axios.get('/workoutExercises');
    return res.data;
}

// Add getById, create, update, delete
