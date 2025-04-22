import { makeObservable, observable, action } from 'mobx';
import { vehicleModelService } from '../Services/vehicleModelService';
import { vehicleModelListStore } from './VehicleModelListStore'; { }

class VehicleModelEditPostDelStore {
  editingVehicle = null;
  newVehicle = { name: '', abrv: '' };

  constructor() {
    makeObservable(this, {
      editingVehicle : observable,
      newVehicle : observable,

      addVehicle: action,
      updateVehicle: action,
      deleteVehicle: action,
      setEditingVehicle: action,
    });
  }

  async addVehicle(vehicle) {
    try {
      await vehicleModelService.create(vehicle)
      this.newVehicle = { name: '', abrv: '', makeId: '' }
      await vehicleModelListStore.fetchVehicles();
    } catch (error) {
      console.error('Error adding vehicle:', error);
    }
  }

  async updateVehicle(vehicle) {
    try {
      await vehicleModelService.update(vehicle);
      this.editingVehicle = null;
      await vehicleModelListStore.fetchVehicles();
    } catch (error) {
      console.error('Error updating vehicle:', error);

    }
  }

  async deleteVehicle(id) {
    try {
      await vehicleModelService.delete(id);
      await vehicleModelListStore.fetchVehicles();
    } catch (error) {
      console.error('Error deleting vehicle:', error);
    }
  }

  setEditingVehicle(vehicle) {
    this.editingVehicle = vehicle
  }

}

export const vehicleModelEditPostDelStore = new VehicleModelEditPostDelStore();