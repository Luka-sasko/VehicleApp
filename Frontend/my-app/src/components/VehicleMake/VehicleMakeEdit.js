import React, {  useState, useEffect } from 'react';
import "../../styles/VehicleMakeEdit.css";

const VehicleMakeEdit = ({ vehicle, onSave, onCancel }) => {
  const [editedVehicle, setEditedVehicle] = useState(vehicle);

  useEffect(() => {
    setEditedVehicle(vehicle);
  }, [vehicle]);

  const handleChange = (e) => {
    setEditedVehicle({ ...editedVehicle, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSave(editedVehicle);
  };

  if (!vehicle) return null; 

  return (
    <div className="edit-form">
      <h3>Edit Vehicle</h3>
      <form onSubmit={handleSubmit}>
        <label>Name:</label>
        <input type="text" name="name" value={editedVehicle.name} onChange={handleChange} />

        <label>Abbreviation:</label>
        <input type="text" name="abrv" value={editedVehicle.abrv} onChange={handleChange} />

        <button type="submit" className="save-btn">Save</button>
        <button type="button" className="cancel-btn" onClick={onCancel}>Cancel</button>
      </form>
    </div>
  );
};

export default VehicleMakeEdit;
