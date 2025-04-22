import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleModelEditPostDelStore } from '../../Stores/VehicleModelEditPostDelStore';

import { vehicleMakeListStore } from '../../Stores/VehicleMakeListStore';
import '../../Styles/VehicleMakeEdit.css';

const VehicleModelPost = observer(() => {
  const [vehicleMakes, setVehicleMakes] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        await vehicleMakeListStore.fetchVehicles();
        setVehicleMakes(vehicleMakeListStore.getNameAndId || []);
      } catch (error) {
        console.error('Failed to fetch vehicle makes:', error);
      }
    };

    fetchData();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    vehicleModelEditPostDelStore.newVehicle = {
      ...vehicleModelEditPostDelStore.newVehicle,
      [name]: value,
    };
  };

  const handleMakeChange = (e) => {
    vehicleModelEditPostDelStore.newVehicle = {
      ...vehicleModelEditPostDelStore.newVehicle,
      makeId: e.target.value,
    };
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const { name, abrv, makeId } = vehicleModelEditPostDelStore.newVehicle;

    if (!name?.trim() || !abrv?.trim() || !makeId) {
      alert("All fields must be filled!");
      return;
    }
    vehicleModelEditPostDelStore.addVehicle(vehicleModelEditPostDelStore.newVehicle);
  };

  return (
    <div className="edit-form">
      <h3>Add new Vehicle Model</h3>
      <form onSubmit={handleSubmit}>
        <label>Name:</label>
        <input
          type="text"
          name="name"
          value={vehicleModelEditPostDelStore.newVehicle?.name || ''}
          onChange={handleChange}
        />

        <label>Abbreviation:</label>
        <input
          type="text"
          name="abrv"
          value={vehicleModelEditPostDelStore.newVehicle?.abrv || ''}
          onChange={handleChange}
        />

        <label>Made by:</label>
        <select
          className="select_label"
          value={vehicleModelEditPostDelStore.newVehicle?.makeId || ''}
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