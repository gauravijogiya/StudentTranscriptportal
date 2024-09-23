import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Login from './components/Login';
import StudentForm from './components/studentForm';


function App() {
  return (
    <Router>
      <Routes>
        {/* Public Route */}
        <Route path="Login" element={<Login />} />

        {/* Private Route (requires authentication) */}
        <Route path="StudentForm" element={<PrivateRoute><StudentForm /></PrivateRoute>} />

        {/* Default Route */}
        <Route path="/" element={<Navigate replace to="/login" />} />
      </Routes>
    </Router>
  );
}

// PrivateRoute component to check for authentication (JWT presence)
const PrivateRoute = ({ children }) => {
  const token = localStorage.getItem('token');
  return token ? children : <Navigate to="/login" />;
};

export default App;
