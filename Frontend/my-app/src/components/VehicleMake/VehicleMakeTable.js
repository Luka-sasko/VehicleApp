import React, { useState, useRef, useEffect } from 'react';
import { del, put } from '../base_api';
import VehicleMakeEdit from "./VehicleMakeEdit";
import "../../styles/VehicleMakeTable.css";

const VehicleMakeTable = ({ vehicles, fetchData }) => {
  const [currentPage, setCurrentPage] = useState(1);
  const [vehiclesPerPage, setVehiclesPerPage] = useState(5);
  const [editingVehicle, setEditingVehicle] = useState(null);

  const indexOfLastVehicle = currentPage * vehiclesPerPage;
  const indexOfFirstVehicle = indexOfLastVehicle - vehiclesPerPage;
  const currentVehicles = vehicles.slice(indexOfFirstVehicle, indexOfLastVehicle);
  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  const formRef = useRef(null);
  useEffect(() => {
    if (editingVehicle && formRef.current) {
      formRef.current.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }, [editingVehicle]);

  useEffect(() => {
    setCurrentPage(1);
  }, [vehiclesPerPage]);

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this vehicle?")) {
      try {
        await del(`/vehiclemake/${id}`);
        fetchData();
      } catch (error) {
        console.error("Error deleting vehicle:", error);
      }
    }
  };

  const handleUpdate = async (updatedVehicle) => {
    try {
      await put(`/vehiclemake/${updatedVehicle.id}`, updatedVehicle);
      console.log("Spremljeno vozilo:", updatedVehicle);
      setEditingVehicle(null);
      fetchData();
    } catch (error) {
      console.error("Greška pri ažuriranju vozila:", error);
    }
  };

  return (
    <div id="body">
      <label>
        Vehicles per page:
        <select value={vehiclesPerPage} onChange={(e) => setVehiclesPerPage(Number(e.target.value))}>
          <option value="1">1</option>
          <option value="2">2</option>
          <option value="5">5</option>
          <option value="10">10</option>
          <option value="20">20</option>
        </select>
      </label>

      <table id="vehicles-table" className="styled-table">
        <thead>
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Abbreviation</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {currentVehicles.map((vehicle, index) => (
            <tr key={vehicle.id}>
              <td>{indexOfFirstVehicle + index + 1}</td>
              <td>{vehicle.name}</td>
              <td>{vehicle.abrv}</td>
              <td>
                <button className="edit-btn" onClick={() => setEditingVehicle(vehicle)}>Edit</button>
                <button className="delete-btn" onClick={() => handleDelete(vehicle.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      <div className="pagination">
        {Array.from({ length: Math.ceil(vehicles.length / vehiclesPerPage) }, (_, i) => (
          <button key={i + 1} onClick={() => paginate(i + 1)} className={currentPage === i + 1 ? "active" : ""}>
            {i + 1}
          </button>
        ))}
      </div>

      {editingVehicle && (
        <div ref={formRef}>
          <VehicleMakeEdit
            vehicle={editingVehicle}
            onSave={(updatedVehicle) => handleUpdate(updatedVehicle)}
            onCancel={() => setEditingVehicle(null)}
          />
        </div>
      )}

    </div>

  );
};

export default VehicleMakeTable;
