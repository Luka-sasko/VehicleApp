import React, {  useState } from 'react';
import "../../styles/VehicleMakeEdit.css";

const VehicleMakePost = ({ vehicle, onSave }) => {

  const [newVehicle, setNewVehicle] = useState(vehicle);

  const handleChange = (e) => {
    setNewVehicle({ ...newVehicle, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (newVehicle.name.trim() === "" || newVehicle.abrv.trim() === "") {
        alert("Sva polja moraju biti popunjena!");
        return;
    }
    onSave(newVehicle);
  };

  if (!vehicle) return null; 

  return (
    <div className="edit-form">
      <h3>New Vehicle</h3>
      <form onSubmit={handleSubmit}>
        <label>Name:</label>
        <input type="text" name="name" value={newVehicle.name} onChange={handleChange} />

        <label>Abbreviation:</label>
        <input type="text" name="abrv" value={newVehicle.abrv} onChange={handleChange} />

        <button type="submit" className="save-btn">Save</button>
      </form>
    </div>
  );
};

export default VehicleMakePost;
