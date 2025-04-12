import React, { useRef, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleMakeStore } from '../../Stores/VehicleMakeStore';
import VehicleMakeEdit from './VehicleMakeEdit';
import '../../Styles/VehicleMakeTable.css';

const VehicleMakeTable = observer(() => {
    const formRef = useRef(null);

    useEffect(() => {
        if (vehicleMakeStore.editingVehicle && formRef.current) {
            formRef.current.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    }, [vehicleMakeStore.editingVehicle]);

    const handleDelete = (id) => {
        if (window.confirm('Are you sure you want to delete this vehicle?')) {
            vehicleMakeStore.deleteVehicle(id);
        }
    };

    return (
        <div id="body">
            <div className="filters">
                <input
                    type="text"
                    placeholder="Search by name"
                    value={vehicleMakeStore.searchName}
                    onChange={(e) => vehicleMakeStore.setSearchName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Search by abbreviation"
                    value={vehicleMakeStore.searchAbrv}
                    onChange={(e) => vehicleMakeStore.setSearchAbrv(e.target.value)}
                />
                <label className="select_label"> Items per page
                    <select
                        className="select_label"
                        value={vehicleMakeStore.vehiclesPerPage}
                        onChange={(e) => vehicleMakeStore.setVehiclesPerPage(Number(e.target.value))}
                    >
                        <option value="5">5</option>
                        <option value="10">10</option>
                        <option value="20">20</option>
                    </select>
                </label>

                <label className="select_label"> Sorted
                    <select
                        className="select_label"
                        value={vehicleMakeStore.sortOrder}
                        onChange={(e) => vehicleMakeStore.setSortOrder(e.target.value)}
                    >
                        <option value="asc">ASC</option>
                        <option value="desc">DESC</option>
                    </select>
                </label>

                <label className="select_label">
                    Sorted
                    <select
                        className="select_label"
                        value={vehicleMakeStore.sortBy}
                        onChange={(e) => vehicleMakeStore.setSortBy(e.target.value)}
                    >
                        <option value="Name">Name</option>
                        <option value="Abrv">Abrv</option>
                    </select>
                </label>
            </div>

            <table id="vehicles-table" className="styled-table">
                <thead>
                    <tr>
                        <th>#</th>
                        <th onClick={() => vehicleMakeStore.setSortBy('Name')}>Name</th>
                        <th onClick={() => vehicleMakeStore.setSortBy('Abrv')}>Abbreviation</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {vehicleMakeStore.vehicles.map((vehicle, index) => (
                        <tr key={vehicle.id}>
                            <td>{(vehicleMakeStore.currentPage - 1) * vehicleMakeStore.vehiclesPerPage + index + 1}</td>
                            <td>{vehicle.name}</td>
                            <td>{vehicle.abrv}</td>
                            <td>
                                <button className="edit-btn" onClick={() => vehicleMakeStore.setEditingVehicle(vehicle)}>
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
                {vehicleMakeStore.totalPages > 0 ? (
                    Array.from({ length: vehicleMakeStore.totalPages }, (_, i) => (
                        <button
                            key={i + 1}
                            onClick={() => vehicleMakeStore.setCurrentPage(i + 1)}
                            className={vehicleMakeStore.currentPage === i + 1 ? 'active' : ''}
                        >
                            {i + 1}
                        </button>
                    ))
                ) : (
                    <span>No pages available</span>
                )}

            </div>

            {vehicleMakeStore.editingVehicle && (
                <div ref={formRef}>
                    <VehicleMakeEdit />
                </div>
            )}

        </div>
    );
});

export default VehicleMakeTable;