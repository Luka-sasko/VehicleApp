import React from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleStore } from '../../Stores/VehicleMakeStore';
import '../../Styles/VehicleMakeEdit.css';

const VehicleMakeEdit = observer(() => {

  const vehicle = vehicleStore.editingVehicle;

  const handleChange = (e) => {
    vehicleStore.editingVehicle = {
      ...vehicleStore.editingVehicle,
      [e.target.name]: e.target.value
    };
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    vehicleStore.updateVehicle(vehicleStore.editingVehicle);
  };

  const handleCancel = () => {
    vehicleStore.setEditingVehicle(null);
  };

  if (!vehicle) return null;

  return (
    <div className="edit-form">
      <h3>Edit Vehicle</h3>
      <form onSubmit={handleSubmit}>
        <label>Name:</label>
        <input 
          type="text" 
          name="name" 
          value={vehicle.name} 
          onChange={handleChange} 
        />

        <label>Abbreviation:</label>
        <input 
          type="text" 
          name="abrv" 
          value={vehicle.abrv} 
          onChange={handleChange} 
        />

        <button type="submit" className="save-btn">Save</button>
        <button type="button" className="cancel-btn" onClick={handleCancel}>
          Cancel
        </button>
      </form>
    </div>
  );
});

export default VehicleMakeEdit;