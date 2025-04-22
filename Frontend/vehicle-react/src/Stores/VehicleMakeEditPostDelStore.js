import { makeObservable, action, observable } from 'mobx';
import { vehicleMakeService } from '../Services/vehicleMakeService';
import { vehicleMakeListStore } from './VehicleMakeListStore';

class VehicleMakeEditPostDelStore {
  editingVehicle = null;
  newVehicle = { name: '', abrv: '' };

  constructor() {
    makeObservable(this, {
      editingVehicle: observable,
      newVehicle:observable,

      addVehicle: action,
      updateVehicle: action,
      deleteVehicle: action,
      setEditingVehicle: action
    });
  }

  async addVehicle(vehicle) {
    try {
      await vehicleMakeService.create(vehicle);
      this.newVehicle = { name: '', abrv: '' };
      await vehicleMakeListStore.fetchVehicles();
    } catch (error) {
      console.error('Error adding vehicle:', error);
    }
  }

  async updateVehicle(vehicle) {
    try {
      await vehicleMakeService.update(vehicle);
      this.editingVehicle = null;
      await vehicleMakeListStore.fetchVehicles();
    } catch (error) {
      console.error('Error updating vehicle:', error);
    }
    
  }

  async deleteVehicle(id) {
    try {
      await vehicleMakeService.delete(id);
      await vehicleMakeListStore.fetchVehicles();
    } catch (error) {
      console.error('Error deleting vehicle:', error);
    }
  }

  setEditingVehicle(vehicle) {
    this.editingVehicle = vehicle;
  }

}

export const vehicleMakeEditPostDelStore = new VehicleMakeEditPostDelStore();