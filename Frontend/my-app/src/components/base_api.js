import axios from "axios";

const BASE_URL = "https://localhost:44351/api";

const get = async (url) => {

    return await axios.get(`${BASE_URL}${url}`);
}

const post = async (url, data) => {

    return await axios.post(`${BASE_URL}${url}`, data);
}

const put = async (url, data) => {
    return await axios.put(`${BASE_URL}${url}`, data);
}

const del = async (url) => {
    
    return await axios.delete(`${BASE_URL}${url}`);
}



export { get, post, put, del};