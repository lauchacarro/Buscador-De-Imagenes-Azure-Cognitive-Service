import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:7112'
});



export default instance;