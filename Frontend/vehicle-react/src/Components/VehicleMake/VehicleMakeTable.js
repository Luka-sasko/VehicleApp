import React, { useRef, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { vehicleMakeListStore } from '../../Stores/VehicleMakeListStore';
import { vehicleMakeEditPostDelStore } from '../../Stores/VehicleMakeEditPostDelStore';
import VehicleMakeEdit from './VehicleMakeEdit';
import '../../Styles/VehicleMakeTable.css';

const VehicleMakeTable = observer(() => {
    const formRef = useRef(null);

    useEffect(() => {
        if (vehicleMakeListStore.editingVehicle && formRef.current) {
            formRef.current.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    }, [vehicleMakeListStore.editingVehicle]);

    const handleDelete = (id) => {
        if (window.confirm('Are you sure you want to delete this vehicle?')) {
            vehicleMakeEditPostDelStore.deleteVehicle(id);
        }
    };

    return (
        <div id="body">
            <div className="filters">
                <input
                    type="text"
                    placeholder="Search by name"
                    value={vehicleMakeListStore.searchName}
                    onChange={(e) => vehicleMakeListStore.setSearchName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Search by abbreviation"
                    value={vehicleMakeListStore.searchAbrv}
                    onChange={(e) => vehicleMakeListStore.setSearchAbrv(e.target.value)}
                />
                <label className="select_label"> Items per page
                    <select
                        className="select_label"
                        value={vehicleMakeListStore.vehiclesPerPage}
                        onChange={(e) => vehicleMakeListStore.setVehiclesPerPage(Number(e.target.value))}
                    >
                        <option value="5">5</option>
                        <option value="10">10</option>
                        <option value="20">20</option>
                    </select>
                </label>

                <label className="select_label"> Sorted
                    <select
                        className="select_label"
                        value={vehicleMakeListStore.sortOrder}
                        onChange={(e) => vehicleMakeListStore.setSortOrder(e.target.value)}
                    >
                        <option value="asc">ASC</option>
                        <option value="desc">DESC</option>
                    </select>
                </label>

                <label className="select_label">
                    Sorted
                    <select
                        className="select_label"
                        value={vehicleMakeListStore.sortBy}
                        onChange={(e) => vehicleMakeListStore.setSortBy(e.target.value)}
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
                        <th onClick={() => vehicleMakeListStore.setSortBy('Name')}>Name</th>
                        <th onClick={() => vehicleMakeListStore.setSortBy('Abrv')}>Abbreviation</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {vehicleMakeListStore.vehicles.map((vehicle, index) => (
                        <tr key={vehicle.id}>
                            <td>{(vehicleMakeListStore.currentPage - 1) * vehicleMakeListStore.vehiclesPerPage + index + 1}</td>
                            <td>{vehicle.name}</td>
                            <td>{vehicle.abrv}</td>
                            <td>
                                <button className="edit-btn" onClick={() => vehicleMakeEditPostDelStore.setEditingVehicle(vehicle)}>
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
                {vehicleMakeListStore.totalPages > 0 ? (
                    Array.from({ length: vehicleMakeListStore.totalPages }, (_, i) => (
                        <button
                            key={i + 1}
                            onClick={() => vehicleMakeListStore.setCurrentPage(i + 1)}
                            className={vehicleMakeListStore.currentPage === i + 1 ? 'active' : ''}
                        >
                            {i + 1}
                        </button>
                    ))
                ) : (
                    <span>No pages available</span>
                )}

            </div>

            {vehicleMakeEditPostDelStore.editingVehicle && (
                <div ref={formRef}>
                    <VehicleMakeEdit />
                </div>
            )}

        </div>
    );
});

export default VehicleMakeTable;