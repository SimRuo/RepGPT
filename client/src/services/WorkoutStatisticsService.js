// services/WorkoutStatisticsService.js
import axios from './api';

export async function getOverallStats(userId) {
    const res = await axios.get(`/workoutstatistics/${userId}`);
    return res.data;
}

export async function getExerciseStats(userId, exerciseName) {
    const res = await axios.get(`/workoutstatistics/exercise/${userId}/${encodeURIComponent(exerciseName)}`);
    return res.data;
}
