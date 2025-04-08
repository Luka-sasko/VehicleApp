import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import VehicleModelTable from '../Components/VehicleModel/VehicleModelTable';
import VehicleModelPost from '../Components/VehicleModel/VehicleModelPost';
import { vehicleModelStore } from '../Stores/VehicleModelStore';
import '../Styles/VehicleMakePage.css';

const VehicleModelPage = observer(() => {
  useEffect(() => {
    vehicleModelStore.fetchVehicles();
  }, []);

  return (
    <div>
      <h2 id="titleTable">VehicleModel Table</h2>
      <VehicleModelPost />
      <VehicleModelTable />
    </div>
  );
});

export default VehicleModelPage;