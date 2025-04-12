import { makeAutoObservable, action } from 'mobx';
import { get, post, put, remove } from '../Api/base_api';

class VehicleModelStore {
    vehicles = [];
    totalCount = 0;
    currentPage = 1;
    vehiclesPerPage = 10;
    editingVehicle = null;
    newVehicle = { name: '', abrv: '' };
    sortBy = 'Name';
    sortOrder = 'asc';
    searchName = '';
    searchAbrv = '';
    searchMakeId = '';

    constructor() {
        makeAutoObservable(this, {
            fetchVehicles: action,
            addVehicle: action,
            updateVehicle: action,
            deleteVehicle: action,
            setCurrentPage: action,
            setVehiclesPerPage: action,
            setEditingVehicle: action,
            setSortBy: action,
            setSortOrder: action,
            setSearchName: action,
            setSearchAbrv: action,
            setSearchMakeId: action,
            setVehicles: action,
            setTotalCount: action
        });
    }

    async fetchVehicles() {
        try {
            const params = new URLSearchParams({
                name: this.searchName,
                abrv: this.searchAbrv,
                makeId: this.searchMakeId,
                sortBy: this.sortBy,
                sortOrder: this.sortOrder,
                pageNumber: this.currentPage,
                pageSize: this.vehiclesPerPage
            });
            const response = await get(`/vehiclemodel?${params.toString()}`);
            this.setVehicles(response.data.items);
            this.setTotalCount(response.data.totalCount);
        } catch (error) {
            this.setVehicles([]);
            this.setTotalCount(0);
            console.error("Error fetching vehicles:", error);
        }
    }

    async addVehicle(vehicle) {
        try {
            await post('/vehiclemodel/', vehicle);
            this.newVehicle = { name: '', abrv: '', makeId: '' }
            await this.fetchVehicles();
        } catch (error) {
            console.error('Error adding vehicle:', error);
        }
    }


    async updateVehicle(vehicle) {
        try {
            await put(`/vehiclemodel/${vehicle.id}`, vehicle);
            this.editingVehicle = null;
            await this.fetchVehicles();
        } catch (error) {
            console.error('Error updating vehicle:', error);

        }
    }


    async deleteVehicle(id) {
        try {
            await remove(`/vehiclemodel/${id}`);
            await this.fetchVehicles();
        } catch (error) {
            console.error('Error deleting vehicle:', error);
        }
    }


    setCurrentPage(page) {
        this.currentPage = page;
        this.fetchVehicles();
    }

    setVehiclesPerPage(count) {
        this.vehiclesPerPage = count;
        this.currentPage = 1;
        this.fetchVehicles();
    }

    setEditingVehicle(vehicle) {
        this.editingVehicle = vehicle
    }

    setSortBy(sort) {
        this.sortBy = sort;
        this.fetchVehicles();
    }

    setSortOrder(order) {
        this.sortOrder = order;
        this.fetchVehicles();
    }

    setSearchName(name) {
        this.searchName = name;
        this.currentPage = 1;
        this.fetchVehicles();
    }

    setSearchAbrv(abrv) {
        this.searchAbrv = abrv;
        this.currentPage = 1;
        this.fetchVehicles();
    }

    setSearchMakeId(makeId) {
        this.searchMakeId = makeId;
        this.currentPage = 1;
        this.fetchVehicles();
    }

    setVehicles(vehicles) {
        this.vehicles = vehicles;
    }

    setTotalCount(total) {
        this.totalCount = total;
    }

    get totalPages() {
        return Math.ceil(this.totalCount / this.vehiclesPerPage) || 1;
    }

}


export const vehicleModelStore = new VehicleModelStore();