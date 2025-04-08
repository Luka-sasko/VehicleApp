import React from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleModelStore } from '../../Stores/VehicleModelStore';
import '../../Styles/VehicleMakeEdit.css';

const VehicleModelEdit = observer(() => { 
    const { editingVehicle } = vehicleModelStore;  
    if (editingVehicle) {  
        return (
            <div>
                <input value={editingVehicle.name} />
            </div>
        );
    }
});

export default VehicleModelEdit;