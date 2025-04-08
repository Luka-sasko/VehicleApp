import React from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleStore } from '../../Stores/VehicleMakeStore';
import '../../Styles/VehicleMakeEdit.css';

const VehicleMakePost = observer(() => {
  const handleChange = (e) => {
    vehicleStore.newVehicle = {
      ...vehicleStore.newVehicle,
      [e.target.name]: e.target.value
    };
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (vehicleStore.newVehicle.name.trim() === "" || vehicleStore.newVehicle.abrv.trim() === "") {
      alert("Sva polja moraju biti popunjena!");
      return;
    }
    vehicleStore.addVehicle(vehicleStore.newVehicle);
  };

  return (
    <div className="edit-form">
      <h3>New Vehicle</h3>
      <form onSubmit={handleSubmit}>
        <label>Name:</label>
        <input 
          type="text" 
          name="name" 
          value={vehicleStore.newVehicle.name} 
          onChange={handleChange} 
        />

        <label>Abbreviation:</label>
        <input 
          type="text" 
          name="abrv" 
          value={vehicleStore.newVehicle.abrv} 
          onChange={handleChange} 
        />

        <button type="submit" className="save-btn">Save</button>
      </form>
    </div>
  );
});

export default VehicleMakePost;