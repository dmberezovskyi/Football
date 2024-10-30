import axios from 'axios';
import qs from 'qs'

// ----------------------------------------------------------------------

const axiosInstance = axios.create();

axiosInstance.interceptors.request.use(
  requst => {
    
    if(requst.method === 'get')
      requst.paramsSerializer = (params) => qs.stringify(params);
      
    return requst;
  },
  error => error
);


export default axiosInstance;
