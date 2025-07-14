import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";

import App from "./App.jsx";
import LoginView from "./views/LoginView.jsx";
import Dashboard from "./views/Dashboard.jsx";
import Home from "./views/Home.jsx";
import NotFound from "./views/NotFound.jsx";

import "./index.css";
import ChatView from "./views/ChatView.jsx";

const requireAuth = (element) => {
  const userId = localStorage.getItem("userId");
  return userId ? element : <LoginView />;
};

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "/", element: <Home /> },
      { path: "/login", element: <LoginView /> },
      { path: "/dashboard", element: requireAuth(<Dashboard />) },
      { path: "/chat", element: requireAuth(<ChatView />) },
      { path: "*", element: <NotFound /> },
    ],
  },
]);

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);
