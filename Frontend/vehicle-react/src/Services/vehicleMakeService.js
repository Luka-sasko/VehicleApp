import { get, post, put, remove } from '../Api/base_api';

const endpoint = '/vehiclemake';

export const vehicleMakeService = {

  async fetchAll(params) {
    const query = new URLSearchParams(params).toString();
    return await get(`${endpoint}?${query}`);
  },

  async create(vehicle) {
    return await post(endpoint + '/', vehicle);
  },

  async update(vehicle) {
    return await put(`${endpoint}/${vehicle.id}`, vehicle);
  },

  async delete(id) {
    return await remove(`${endpoint}/${id}`);
  }
};

