import axios from './api';

export async function getAllPlans() {
    const res = await axios.get('/workoutPlans');
    return res.data;
}

export async function getPlanById(id) {
    const res = await axios.get(`/workoutPlans/${id}`);
    return res.data;
}

export async function createPlan(dto) {
    const res = await axios.post('/workoutPlans', dto);
    return res.data;
}

export async function updatePlan(id, dto) {
    const res = await axios.put(`/workoutPlans/${id}`, dto);
    return res.data;
}

export async function deletePlan(id) {
    const res = await axios.delete(`/workoutPlans/${id}`);
    return res.data;
}

export async function generatePlan(goal, userId) {
    const dto = { goal, userId };
    const res = await axios.post('/workoutPlans/generateOnly', dto);
    return res.data;
}
