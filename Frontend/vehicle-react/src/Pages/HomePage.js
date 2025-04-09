
import React from 'react';
import '../Styles/HomePage.css';

const HomePage = () => {
  return (
    <div className="home-container">
      <h1>Dobrodošli u Vehicle App!</h1>
      <p>Ova aplikacija omogućuje upravljanje markama i modelima vozila.</p>

      <div className="home-buttons">
        <a href="/vehicle-makes" className="home-btn">Pregledaj Marke Vozila</a>
        <a href="/vehicle-models" className="home-btn">Pregledaj Modele Vozila</a>
      </div>
    </div>
  );
};

export default HomePage;
