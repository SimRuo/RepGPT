import axios from './api';

export async function getPromptHistory() {
    const res = await axios.get('/promptHistory');
    return res.data;
}

export async function addPrompt(userId, promptText, responseText) {
    const res = await axios.post('/promptHistory', {
        userId,
        promptText,
        responseText,
        createdAt: new Date().toISOString()
    });
    return res.data;
}
