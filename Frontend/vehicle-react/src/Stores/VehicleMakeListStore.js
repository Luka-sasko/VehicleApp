import { makeObservable, action, observable, computed } from 'mobx';
import { vehicleMakeService } from '../Services/vehicleMakeService';

class VehicleMakeListStore {
  vehicles = [];
  totalCount = 0;
  currentPage = 1;
  vehiclesPerPage = 10;
  sortBy = 'Name';
  sortOrder = 'asc';
  searchName = '';
  searchAbrv = '';

  constructor() {
    makeObservable(this, {
      vehicles : observable,
      totalCount : observable,
      currentPage : observable,
      vehiclesPerPage : observable,
      sortBy : observable,
      sortOrder : observable,
      searchAbrv : observable,
      searchName : observable,

      fetchVehicles: action,
      setCurrentPage: action,
      setVehiclesPerPage: action,
      setSortBy: action,
      setSortOrder: action,
      setSearchName: action,
      setSearchAbrv: action,
      setVehicles: action,
      setTotalCount: action,

      totalPages : computed
    });
  }

  async fetchVehicles() {
    try {
      const params = new URLSearchParams({
        name: this.searchName,
        abrv: this.searchAbrv,
        sortBy: this.sortBy,
        sortOrder: this.sortOrder,
        pageNumber: this.currentPage,
        pageSize: this.vehiclesPerPage
      });

      const response = await vehicleMakeService.fetchAll(params);
      this.setVehicles(response.data.items);
      this.setTotalCount(response.data.totalCount);
    } catch (error) {
      this.setVehicles([]);
      this.setTotalCount(0);
      console.error('Error fetching vehicles:', error);
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
    console.log('Sorting by:', sort);
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

  setVehicles(vehicles) {
    this.vehicles = vehicles;
  }

  setTotalCount(total) {
    this.totalCount = total;
  }

  get getNameAndId() {
    return this.vehicles.map(v => ({
      name: v.name,
      id: v.id
    }));
  }

  get totalPages() {
    return Math.ceil(this.totalCount / this.vehiclesPerPage) || 1;
  }
}

export const vehicleMakeListStore = new VehicleMakeListStore();