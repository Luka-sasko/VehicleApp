import React, { useRef, useEffect, useState } from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleModelStore } from '../../Stores/VehicleModelStore';
import { vehicleMakeStore } from '../../Stores/VehicleMakeStore';
import VehicleModelEdit from './VehicleModelEdit';
import '../../Styles/VehicleMakeTable.css';

const VehicleModelTable = observer(() => {
    const [vehicleMakes, setVehicleMakes] = useState([]);
    const formRef = useRef(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                await vehicleMakeStore.fetchVehicles();
                setVehicleMakes(vehicleMakeStore.getNameAndId || []);
            } catch (error) {
                console.error('Failed to fetch vehicle makes:', error);
            }
        };

        fetchData();
    }, []);

    useEffect(() => {
        if (vehicleModelStore.editingVehicle && formRef.current) {
            formRef.current.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    }, [vehicleModelStore.editingVehicle]);

    const handleDelete = (id) => {
        if (window.confirm('Are you sure you want to delete this vehicle?')) {
            vehicleModelStore.deleteVehicle(id);
        }
    };

    return (
        <div id="body">
            <div className="filters">
                <input
                    type="text"
                    placeholder="Search by name"
                    value={vehicleModelStore.searchName}
                    onChange={(e) => vehicleModelStore.setSearchName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Search by abbreviation"
                    value={vehicleModelStore.searchAbrv}
                    onChange={(e) => vehicleModelStore.setSearchAbrv(e.target.value)}
                />

                <select
                    className="select_label"
                    value={vehicleModelStore.searchMakeId}
                    onChange={(e) => vehicleModelStore.setSearchMakeId(e.target.value)}
                >
                    <option value="">All producers</option>
                    {vehicleMakes.map((make) => (
                        <option key={make.id} value={make.id}>
                            {make.name}
                        </option>
                    ))}
                </select>

                <label className="select_label">
                    Items per page
                    <select
                        className="select_label"
                        value={vehicleModelStore.vehiclesPerPage}
                        onChange={(e) => vehicleModelStore.setVehiclesPerPage(Number(e.target.value))}
                    >
                        <option value="5">5</option>
                        <option value="10">10</option>
                        <option value="20">20</option>
                    </select>
                </label>

                <label className="select_label">
                    Sorted
                    <select
                        className="select_label"
                        value={vehicleModelStore.sortOrder}
                        onChange={(e) => vehicleModelStore.setSortOrder(e.target.value)}
                    >
                        <option value="asc">ASC</option>
                        <option value="desc">DESC</option>
                    </select>
                </label>

                <label className="select_label">
                    Sorted
                    <select
                        className="select_label"
                        value={vehicleModelStore.sortBy}
                        onChange={(e) => vehicleModelStore.setSortBy(e.target.value)}
                    >
                        <option value="Name">Name</option>
                        <option value="Abrv">Abrv</option>
                        <option value="MakeId">Maker</option>
                    </select>
                </label>
            </div>

            <table id="vehicles-table" className="styled-table">
                <thead>
                    <tr>
                        <th>#</th>
                        <th onClick={() => vehicleModelStore.setSortBy('MakeId')}>Made by</th>
                        <th onClick={() => vehicleModelStore.setSortBy('Name')}>Name</th>
                        <th onClick={() => vehicleModelStore.setSortBy('Abrv')}>Abbreviation</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {vehicleModelStore.vehicles.map((vehicle, index) => (
                        <tr key={vehicle.id}>
                            <td>{(vehicleModelStore.currentPage - 1) * vehicleModelStore.vehiclesPerPage + index + 1}</td>
                            <td>{vehicleMakes.find(make => make.id === vehicle.makeId)?.name || vehicle.makeId}</td>
                            <td>{vehicle.name}</td>
                            <td>{vehicle.abrv}</td>
                            <td>
                                <button className="edit-btn" onClick={() => vehicleModelStore.setEditingVehicle(vehicle)}> Edit</button>
                                <button className="delete-btn" onClick={() => handleDelete(vehicle.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <div className="pagination">
                {vehicleModelStore.totalPages > 0 ? (
                    Array.from({ length: vehicleModelStore.totalPages }, (_, i) => (
                        <button
                            key={i + 1}
                            onClick={() => vehicleModelStore.setCurrentPage(i + 1)}
                            className={vehicleModelStore.currentPage === i + 1 ? 'active' : ''}
                        >
                            {i + 1}
                        </button>
                    ))
                ) : (
                    <span>No pages available</span>
                )}
            </div>

            {vehicleModelStore.editingVehicle && (
                <div ref={formRef}>
                    <VehicleModelEdit />
                </div>
            )}
        </div>
    );
});

export default VehicleModelTable;