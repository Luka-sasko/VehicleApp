import React, { useEffect, useState } from 'react';
import { get } from '../components/base_api'; 

const VehicleList = () => {
    const [vehicles, setVehicles] = useState([]); 

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await get("/vehiclemake"); 
                console.log(response.data);
                setVehicles(response.data); 
            } catch (error) {
                console.error("Error fetching data:", error); 
            }
        };

        fetchData(); 
    }, []);

    return (
        <div>
            <h1>Popis proizvođača vozila  iz baze podataka</h1>
            <ul>
                {vehicles.map((vehicle) => (
                    <li key={vehicle.id}>{vehicle.name} {vehicle.abrv}</li> 
                ))}
            </ul>
        </div>
    );
};

export default VehicleList;
