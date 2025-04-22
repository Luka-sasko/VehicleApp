import { makeObservable, observable, action, computed } from 'mobx';import { get } from '../Api/base_api';

class VehicleModelListStore {
    vehicles = [];
    totalCount = 0;
    currentPage = 1;
    vehiclesPerPage = 10;
    sortBy = 'Name';
    sortOrder = 'asc';
    searchName = '';
    searchAbrv = '';
    searchMakeId = '';

    constructor() {
        makeObservable(this, {
            vehicles: observable,
            totalCount: observable,
            currentPage: observable,
            vehiclesPerPage: observable,
            sortBy: observable,
            sortOrder: observable,
            searchName: observable,
            searchAbrv: observable,
            searchMakeId: observable,

            fetchVehicles: action,
            setCurrentPage: action,
            setVehiclesPerPage: action,
            setSortBy: action,
            setSortOrder: action,
            setSearchName: action,
            setSearchAbrv: action,
            setSearchMakeId: action,
            setVehicles: action,
            setTotalCount: action,

            totalPages: computed
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

    setCurrentPage(page) {
        this.currentPage = page;
        this.fetchVehicles();
    }

    setVehiclesPerPage(count) {
        this.vehiclesPerPage = count;
        this.currentPage = 1;
        this.fetchVehicles();
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

export const vehicleModelListStore = new VehicleModelListStore();