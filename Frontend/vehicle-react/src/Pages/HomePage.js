import React from 'react';
import { Link } from 'react-router-dom';
import '../Styles/HomePage.css';

const HomePage = () => {
    return (
        <div className="home-container">
            <h1>Welcome to the Vehicle App!</h1>
            <p>Manage vehicle makes and models quickly and easily.</p>

            <div className="home-buttons">
                <Link to="/vehicle-makes" className="home-btn" >
                    View Vehicle Makes
                </Link>
                <Link to="/vehicle-models" className="home-btn" >
                    View Vehicle Models
                </Link>
            </div>

            <div className="features">
                <ul>
                    <li>✔️ Add, edit and delete vehicle makes and models</li>
                    <li>✔️ Filter and search models by make</li>
                    <li>✔️ Paginated and responsive layout</li>
                </ul>
            </div>

        </div>
    );
};

export default HomePage;
