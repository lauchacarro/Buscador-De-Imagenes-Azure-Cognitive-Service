import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://buscador-de-imagenes.azurewebsites.net'
});



export default instance;