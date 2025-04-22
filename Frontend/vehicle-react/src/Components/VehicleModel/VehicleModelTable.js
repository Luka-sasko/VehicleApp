import React, { useRef, useEffect, useState } from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleModelListStore } from '../../Stores/VehicleModelListStore';
import { vehicleModelEditPostDelStore } from '../../Stores/VehicleModelEditPostDelStore';

import { vehicleMakeListStore } from '../../Stores/VehicleMakeListStore';
import VehicleModelEdit from './VehicleModelEdit';
import '../../Styles/VehicleMakeTable.css';

const VehicleModelTable = observer(() => {
    const [vehicleMakes, setVehicleMakes] = useState([]);
    const formRef = useRef(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                await vehicleMakeListStore.fetchVehicles();
                setVehicleMakes(vehicleMakeListStore.getNameAndId || []);
            } catch (error) {
                console.error('Failed to fetch vehicle makes:', error);
            }
        };

        fetchData();
    }, []);

    useEffect(() => {
        if (vehicleModelEditPostDelStore.editingVehicle && formRef.current) {
            formRef.current.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    }, [vehicleModelEditPostDelStore.editingVehicle]);

    const handleDelete = (id) => {
        if (window.confirm('Are you sure you want to delete this vehicle?')) {
            vehicleModelEditPostDelStore.deleteVehicle(id);
        }
    };

    return (
        <div id="body">
            <div className="filters">
                <input
                    type="text"
                    placeholder="Search by name"
                    value={vehicleModelListStore.searchName}
                    onChange={(e) => vehicleModelListStore.setSearchName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Search by abbreviation"
                    value={vehicleModelListStore.searchAbrv}
                    onChange={(e) => vehicleModelListStore.setSearchAbrv(e.target.value)}
                />

                <select
                    className="select_label"
                    value={vehicleModelListStore.searchMakeId}
                    onChange={(e) => vehicleModelListStore.setSearchMakeId(e.target.value)}
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
                        value={vehicleModelListStore.vehiclesPerPage}
                        onChange={(e) => vehicleModelListStore.setVehiclesPerPage(Number(e.target.value))}
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
                        value={vehicleModelListStore.sortOrder}
                        onChange={(e) => vehicleModelListStore.setSortOrder(e.target.value)}
                    >
                        <option value="asc">ASC</option>
                        <option value="desc">DESC</option>
                    </select>
                </label>

                <label className="select_label">
                    Sorted
                    <select
                        className="select_label"
                        value={vehicleModelListStore.sortBy}
                        onChange={(e) => vehicleModelListStore.setSortBy(e.target.value)}
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
                        <th onClick={() => vehicleModelListStore.setSortBy('MakeId')}>Made by</th>
                        <th onClick={() => vehicleModelListStore.setSortBy('Name')}>Name</th>
                        <th onClick={() => vehicleModelListStore.setSortBy('Abrv')}>Abbreviation</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {vehicleModelListStore.vehicles.map((vehicle, index) => (
                        <tr key={vehicle.id}>
                            <td>{(vehicleModelListStore.currentPage - 1) * vehicleModelListStore.vehiclesPerPage + index + 1}</td>
                            <td>{vehicleMakes.find(make => make.id === vehicle.makeId)?.name || vehicle.makeId}</td>
                            <td>{vehicle.name}</td>
                            <td>{vehicle.abrv}</td>
                            <td>
                                <button className="edit-btn" onClick={() => vehicleModelEditPostDelStore.setEditingVehicle(vehicle)}> Edit</button>
                                <button className="delete-btn" onClick={() => handleDelete(vehicle.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <div className="pagination">
                {vehicleModelListStore.totalPages > 0 ? (
                    Array.from({ length: vehicleModelListStore.totalPages }, (_, i) => (
                        <button
                            key={i + 1}
                            onClick={() => vehicleModelListStore.setCurrentPage(i + 1)}
                            className={vehicleModelListStore.currentPage === i + 1 ? 'active' : ''}
                        >
                            {i + 1}
                        </button>
                    ))
                ) : (
                    <span>No pages available</span>
                )}
            </div>

            {vehicleModelEditPostDelStore.editingVehicle && (
                <div ref={formRef}>
                    <VehicleModelEdit />
                </div>
            )}
        </div>
    );
});

export default VehicleModelTable;