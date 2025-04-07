import axios from "axios";

const BASE_URL = "https://localhost:44351/api";

const get = async (url) => {
    try {
      return await axios.get(`${BASE_URL}${url}`);
    } catch (error) {
      handleError(error);
    }
  };
  
  const post = async (url, data) => {
    try {
      return await axios.post(`${BASE_URL}${url}`, data);
    } catch (error) {
      handleError(error);
    }
  };
  
  const put = async (url, data) => {
    try {
      return await axios.put(`${BASE_URL}${url}`, data);
    } catch (error) {
      handleError(error);
    }
  };
  
  const del = async (url) => {
    try {
      return await axios.delete(`${BASE_URL}${url}`);
    } catch (error) {
      handleError(error);
    }
  };
  
  const handleError = (error) => {
    console.error("API error:", error);
    throw error; 
  };
  



export { get, post, put, del};