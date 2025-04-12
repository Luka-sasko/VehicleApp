import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleModelStore } from '../../Stores/VehicleModelStore';
import { vehicleMakeStore } from '../../Stores/VehicleMakeStore';
import '../../Styles/VehicleMakeEdit.css';

const VehicleModelPost = observer(() => {
  const [vehicleMakes, setVehicleMakes] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        await vehicleMakeStore.fetchVehicles();
        setVehicleMakes(vehicleMakeStore.getNameAndId || []);
      } catch (error) {
        console.error('Failed to fetch vehicle makes:', error);
      }
    };

    fetchData();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    vehicleModelStore.newVehicle = {
      ...vehicleModelStore.newVehicle,
      [name]: value,
    };
  };

  const handleMakeChange = (e) => {
    vehicleModelStore.newVehicle = {
      ...vehicleModelStore.newVehicle,
      makeId: e.target.value,
    };
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const { name, abrv, makeId } = vehicleModelStore.newVehicle;

    if (!name?.trim() || !abrv?.trim() || !makeId) {
      alert("All fields must be filled!");
      return;
    }
    vehicleModelStore.addVehicle(vehicleModelStore.newVehicle);
  };

  return (
    <div className="edit-form">
      <h3>Add new Vehicle Model</h3>
      <form onSubmit={handleSubmit}>
        <label>Name:</label>
        <input
          type="text"
          name="name"
          value={vehicleModelStore.newVehicle?.name || ''}
          onChange={handleChange}
        />

        <label>Abbreviation:</label>
        <input
          type="text"
          name="abrv"
          value={vehicleModelStore.newVehicle?.abrv || ''}
          onChange={handleChange}
        />

        <label>Made by:</label>
        <select
          className="select_label"
          value={vehicleModelStore.newVehicle?.makeId || ''}
          onChange={handleMakeChange}
          name="makeId"
        >
          <option value="">Select a make</option>
          {vehicleMakes.map((make) => (
            <option key={make.id} value={make.id}>
              {make.name}
            </option>
          ))}
        </select>

        <button type="submit" className="save-btn">Save</button>
      </form>
    </div>
  );
});

export default VehicleModelPost;