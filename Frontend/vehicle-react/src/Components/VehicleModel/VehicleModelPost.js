import React from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleModelStore } from '../../Stores/VehicleModelStore';
import '../../Styles/VehicleMakeEdit.css';

const VehicleModelPost = observer(() => {


    const handleChange = (e) => {
        vehicleModelStore.newVehicle = {
            ...vehicleModelStore.newVehicle,
            [e.target.name]: e.target.value
        };
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (vehicleModelStore.newVehicle.name.trim() === "" || vehicleModelStore.newVehicle.abrv.trim() === "") {
            alert("Sva polja moraju biti popunjena!");
            return;
        }
        vehicleModelStore.addVehicle(vehicleModelStore.newVehicle);
    };

    return (
        <div className="edit-form">
            <h3>New Vehicle Model</h3>
            <form onSubmit={handleSubmit}>
                <label>Name:</label>
                <input
                    type="text"
                    name="name"
                    value={vehicleModelStore.newVehicle.name}
                    onChange={handleChange}
                />

                <label>Abbreviation:</label>
                <input
                    type="text"
                    name="abrv"
                    value={vehicleModelStore.newVehicle.abrv}
                    onChange={handleChange}
                />

                <label>Made by:</label>
                <input
                    type="text"
                    name="makeId"
                    value={vehicleModelStore.newVehicle.makeId}
                    onChange={handleChange}
                />

                <button type="submit" className="save-btn">Save</button>
            </form>
        </div>
    );

});

export default VehicleModelPost;