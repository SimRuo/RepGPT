import axios from 'axios';

const instance = axios.create({
    baseURL: 'http://localhost:5155/api', // switch this for azure endpoint later
    headers: {
        'Content-Type': 'application/json',
    }
});

export default instance;