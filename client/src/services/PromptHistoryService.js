import axios from './api';

export async function getPromptHistory() {
    const res = await axios.get('/promptHistory');
    return res.data;
}

export async function addPrompt(prompt) {
    const res = await axios.post('/promptHistory', { prompt });
    return res.data;
}
