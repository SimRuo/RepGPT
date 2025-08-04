import axios from './api';

export async function getWorkoutLogs() {
    const res = await axios.get('/workoutLogs');
    return res.data;
}
export async function createWorkoutLog(dto) {
    const res = await axios.post("/workoutLogs", dto);
    return res.data;
}
export async function getWorkoutLogsForUser(userId) {
    const res = await axios.get(`/workoutLogs/user/${userId}`);
    return res.data;
}

// Add getById, update, delete
