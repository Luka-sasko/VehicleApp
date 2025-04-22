import React, { useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import VehicleMakeTable from '../Components/VehicleMake/VehicleMakeTable';
import VehicleMakePost from '../Components/VehicleMake/VehicleMakePost';
import { vehicleMakeListStore } from '../Stores/VehicleMakeListStore';
import '../Styles/VehicleMakePage.css';

const VehicleMakePage = observer(() => {
  useEffect(() => {
    vehicleMakeListStore.fetchVehicles();
  }, []);

  return (
    <div>
      <h2 id="titleTable">VehicleMake Table</h2>
      <VehicleMakePost />
      <VehicleMakeTable />
    </div>
  );
});

export default VehicleMakePage;