import './App.css';

import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import VehicleMakePage from "./pages/VehicleMakePage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<h1>Welcome</h1>} />
        <Route path="/vehicle-makes" element={<VehicleMakePage />} />
      </Routes>
    </Router>
  );
}

export default App;
