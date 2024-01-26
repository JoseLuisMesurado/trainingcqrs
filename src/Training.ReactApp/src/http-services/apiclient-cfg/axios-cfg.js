import axios from 'axios';
import initInterceptors from './interceptors';

const api = axios.create({
    timeout: 60000,
    headers: {
        "Content-type": "application/json"
      }
});

initInterceptors(api);

export default api;