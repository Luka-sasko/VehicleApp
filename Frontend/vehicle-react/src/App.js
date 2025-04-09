import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Navbar from "./Components/NavBar";
import VehicleMakePage from "./Pages/VehicleMakePage";
import VehicleModelPage from "./Pages/VehicleModelPage";
import HomePage from "./Pages/HomePage";

function App() {
  return (
    <Router>
      <div className="App">
        <Navbar />
        <Routes>
          <Route path="/" element={<HomePage/>} />
          <Route path="/vehicle-makes" element={<VehicleMakePage />} />
          <Route path="/vehicle-models" element={<VehicleModelPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
