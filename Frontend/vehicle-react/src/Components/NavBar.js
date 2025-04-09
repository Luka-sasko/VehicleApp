import React from 'react';
import { Link } from 'react-router-dom';
import '../Styles/Navbar.css';

const Navbar = () => {
  return (
    <nav className="navbar">
      <div className="navbar-logo">
        <Link to="/">Home</Link>
      </div>
      <ul className="navbar-links">
        <li>
          <Link to="/">Home</Link>
        </li>
        <li>
          <Link to="/vehicleMake">VehicleMake</Link>
        </li>
        <li>
          <Link to="/vehicleModel">VehicleModel</Link>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
