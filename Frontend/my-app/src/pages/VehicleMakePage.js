import React, { useState, useEffect } from 'react';
import VehicleMakeTable from "../components/VehicleMake/VehicleMakeTable";
import VehicleMakePost from "../components/VehicleMake/VehicleMakePost";
import { post, get } from "../components/base_api";
import "../styles/VehicleMakePage.css";

const VehicleMakePage = () => {
    const [newVehicle] = useState({ name: "", abrv: "" });
    const [vehicles, setVehicles] = useState([]);

    const fetchData = async () => {
        try {
            const response = await get("/vehiclemake");
            setVehicles(response.data);
        } catch (error) {
            console.error("Greška pri dohvaćanju vozila:", error);
        }
    };

    useEffect(() => {
        fetchData();
    }, []);

    const handleSave = async (newVehicle) => {
        try {
            await post(`/vehiclemake/`, newVehicle);
            console.log("Spremljeno vozilo:", newVehicle);
            newVehicle.name="";
            newVehicle.abrv="";
            fetchData(); 
        } catch (error) {
            console.error("Greška pri spremanju vozila:", error);
        }
    };

    return (
        <div>
            <h2 id="titleTable">VehicleMake Table</h2>
            <VehicleMakePost vehicle={newVehicle} onSave={handleSave} />
            <VehicleMakeTable vehicles={vehicles} fetchData={fetchData} />
        </div>
    );
};

export default VehicleMakePage;
