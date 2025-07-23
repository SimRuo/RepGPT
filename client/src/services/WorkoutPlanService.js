import axios from './api';

export async function getAllPlans() {
    const res = await axios.get('/workoutPlans');
    return res.data;
}

export async function getPlanById(id) {
    const res = await axios.get(`/workoutPlans/${id}`);
    return res.data;
}
export async function createPlan(planDto, userId) {
    const response = await axios.post("/workoutPlans", {
        ...planDto,
        userId
    });

    return response.data;
}

export async function updatePlan(id, dto) {
    const res = await axios.put(`/workoutPlans/${id}`, dto);
    return res.data;
}

export async function deletePlan(id) {
    const res = await axios.delete(`/workoutPlans/${id}`);
    return res.data;
}


export async function generatePlan(prompt) {
    const dto = { prompt };
    const res = await axios.post('/workoutPlans/generate-only', dto);
    return res.data;
}

export async function getPlansForUser(userId) {
    const res = await axios.get(`/workoutPlans/user/${userId}`);
    return res.data;
}

export async function applyProgressiveOverload(planId) {
    const res = await axios.post(`/workoutPlans/${planId}/progress`);
    return res.data;
}