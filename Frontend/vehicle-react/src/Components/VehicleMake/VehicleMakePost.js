import React from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleMakeEditPostDelStore } from '../../Stores/VehicleMakeEditPostDelStore';
import '../../Styles/VehicleMakeEdit.css';

const VehicleMakePost = observer(() => {
  const handleChange = (e) => {
    vehicleMakeEditPostDelStore.newVehicle = {
      ...vehicleMakeEditPostDelStore.newVehicle,
      [e.target.name]: e.target.value
    };
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (vehicleMakeEditPostDelStore.newVehicle.name.trim() === "" || vehicleMakeEditPostDelStore.newVehicle.abrv.trim() === "") {
      alert("Sva polja moraju biti popunjena!");
      return;
    }
    vehicleMakeEditPostDelStore.addVehicle(vehicleMakeEditPostDelStore.newVehicle);

  };

  return (
    <div className="edit-form">
      <h3>Add new  Vehicle Make</h3>
      <form onSubmit={handleSubmit}>
        <label>Name:</label>
        <input
          type="text"
          name="name"
          value={vehicleMakeEditPostDelStore.newVehicle.name}
          onChange={handleChange}
        />

        <label>Abbreviation:</label>
        <input
          type="text"
          name="abrv"
          value={vehicleMakeEditPostDelStore.newVehicle.abrv}
          onChange={handleChange}
        />

        <button type="submit" className="save-btn">Save</button>
      </form>
    </div>
  );
});

export default VehicleMakePost;