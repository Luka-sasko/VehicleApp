import React from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleMakeStore } from '../../Stores/VehicleMakeStore';
import '../../Styles/VehicleMakeEdit.css';

const VehicleMakePost = observer(() => {
  const handleChange = (e) => {
    vehicleMakeStore.newVehicle = {
      ...vehicleMakeStore.newVehicle,
      [e.target.name]: e.target.value
    };
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (vehicleMakeStore.newVehicle.name.trim() === "" || vehicleMakeStore.newVehicle.abrv.trim() === "") {
      alert("Sva polja moraju biti popunjena!");
      return;
    }
    vehicleMakeStore.addVehicle(vehicleMakeStore.newVehicle);

  };

  return (
    <div className="edit-form">
      <h3>Add new  Vehicle Make</h3>
      <form onSubmit={handleSubmit}>
        <label>Name:</label>
        <input
          type="text"
          name="name"
          value={vehicleMakeStore.newVehicle.name}
          onChange={handleChange}
        />

        <label>Abbreviation:</label>
        <input
          type="text"
          name="abrv"
          value={vehicleMakeStore.newVehicle.abrv}
          onChange={handleChange}
        />

        <button type="submit" className="save-btn">Save</button>
      </form>
    </div>
  );
});

export default VehicleMakePost;