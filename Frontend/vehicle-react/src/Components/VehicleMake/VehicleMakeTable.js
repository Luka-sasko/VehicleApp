import React, { useRef, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleStore } from '../../Stores/VehicleMakeStore';
import VehicleMakeEdit from './VehicleMakeEdit';
import '../../Styles/VehicleMakeTable.css';

const VehicleMakeTable = observer(() => {
    const formRef = useRef(null);

    useEffect(() => {
        if (vehicleStore.editingVehicle && formRef.current) {
            formRef.current.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    }, [vehicleStore.editingVehicle]);

    const handleDelete = (id) => {
        if (window.confirm('Are you sure you want to delete this vehicle?')) {
            vehicleStore.deleteVehicle(id);
        }
    };

    return (
        <div id="body">
            <div className="filters">
                <input
                    type="text"
                    placeholder="Filter by name"
                    value={vehicleStore.searchName}
                    onChange={(e) => vehicleStore.setSearchName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Filter by abbreviation"
                    value={vehicleStore.searchAbrv}
                    onChange={(e) => vehicleStore.setSearchAbrv(e.target.value)}
                />
                <label className ="select_label"> Items per page 
                <select
                    className ="select_label"
                    value={vehicleStore.vehiclesPerPage}
                    onChange={(e) => vehicleStore.setVehiclesPerPage(Number(e.target.value))}
                >
                    <option value="5">5</option>
                    <option value="10">10</option>
                    <option value="20">20</option>
                </select>
                </label>

                <label className ="select_label"> Sorted
                <select
                    className ="select_label"
                    value={vehicleStore.sortOrder}
                    onChange={(e) => vehicleStore.setSortOrder(e.target.value)}
                >
                    <option value="asc">ASC</option>
                    <option value="desc">DESC</option>
                </select>
                </label>
            </div>

            <table id="vehicles-table" className="styled-table">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>
                            Name
                            <button onClick={() => {
                                vehicleStore.setSortBy('Name');
                                vehicleStore.setSortOrder(vehicleStore.sortOrder === 'asc' ? 'desc' : 'asc');
                            }}>
                                
                            </button>
                        </th>
                        <th>
                            Abbreviation
                            <button onClick={() => {
                                vehicleStore.setSortBy('Abrv');
                                vehicleStore.setSortOrder(vehicleStore.sortOrder === 'asc' ? 'desc' : 'asc');
                            }}>
                                
                            </button>
                        </th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {vehicleStore.vehicles.map((vehicle, index) => (
                        <tr key={vehicle.id}>
                            <td>{(vehicleStore.currentPage - 1) * vehicleStore.vehiclesPerPage + index + 1}</td>
                            <td>{vehicle.name}</td>
                            <td>{vehicle.abrv}</td>
                            <td>
                                <button className="edit-btn" onClick={() => vehicleStore.setEditingVehicle(vehicle)}>
                                    Edit
                                </button>
                                <button className="delete-btn" onClick={() => handleDelete(vehicle.id)}>
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <div className="pagination">
                {vehicleStore.totalPages > 0 ? (
                    Array.from({ length: vehicleStore.totalPages }, (_, i) => (
                        <button
                            key={i + 1}
                            onClick={() => vehicleStore.setCurrentPage(i + 1)}
                            className={vehicleStore.currentPage === i + 1 ? 'active' : ''}
                        >
                            {i + 1}
                        </button>
                    ))
                ) : (
                    <span>No pages available</span>
                )}
                
            </div>

            {vehicleStore.editingVehicle && (
                <div ref={formRef}>
                    <VehicleMakeEdit />
                </div>
            )}

        </div>
    );
});

export default VehicleMakeTable;