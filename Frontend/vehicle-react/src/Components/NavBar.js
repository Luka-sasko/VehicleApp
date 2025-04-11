// Navbar.js
import React from 'react';
import { NavLink } from 'react-router-dom';
import '../Styles/Navbar.css';

const Navbar = () => {
  return (
    <nav className="navbar">
      <ul className="navbar-links">
        <li>
          <NavLink to="/" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"}>Home</NavLink>
        </li>
        <li>
          <NavLink to="/vehicle-makes" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"}>Vehicle Make</NavLink>
        </li>
        <li>
          <NavLink to="/vehicle-models" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"}>Vehicle Model</NavLink>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
