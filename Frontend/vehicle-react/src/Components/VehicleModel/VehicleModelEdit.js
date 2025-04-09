import React from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleModelStore } from '../../Stores/VehicleModelStore';
import '../../Styles/VehicleMakeEdit.css';

const VehicleModelEdit = observer(() => {
    const vehicle = vehicleModelStore.editingVehicle;

    const handleChange = (e) => {
        vehicleModelStore.editingVehicle = {
            ...vehicleModelStore.editingVehicle,
            [e.target.name]: e.target.value
        };
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        vehicleModelStore.updateVehicle(vehicleModelStore.editingVehicle);
    };

    const handleCancel = () => {
        vehicleModelStore.setEditingVehicle(null);
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

export default VehicleModelEdit;