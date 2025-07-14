import "./App.css";
import { useState } from "react";
import { Outlet } from "react-router-dom";
import TempDbViewer from "./components/TempDbViewer";
import LoginView from "./views/LoginView";
function App() {
  const [user, setUser] = useState(null);

  return (
    <>
      {/* Header and other stuff goes here later */}
      <Outlet />
    </>
  );
}

export default App;
