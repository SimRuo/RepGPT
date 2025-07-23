import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider, redirect } from "react-router-dom";

import App from "./App.jsx";
import LoginView from "./views/LoginView.jsx";
import Dashboard from "./views/Dashboard.jsx";
import NotFound from "./views/NotFound.jsx";
import ChatView from "./views/ChatView.jsx";
import FrontPage from "./views/FrontPage.jsx";
import { getUserState } from "./services/getUserState";
import RegisterView from "./views/RegisterView.jsx";

import "./index.css";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "/",
        element: <FrontPage />,
        loader: async () => {
          const { status } = await getUserState();
          if (status === "chat") return redirect("/chat");
          if (status === "dashboard") return redirect("/dashboard");
          return null;
        },
      },
      { path: "/login", element: <LoginView /> },
      {
        path: "/dashboard",
        element: <Dashboard />,
        loader: async () => {
          const { status } = await getUserState();
          if (status === "unauthenticated") return redirect("/");
          if (status === "chat") return redirect("/chat");
          return null;
        },
      },
      {
        path: "/chat",
        element: <ChatView />,
        loader: async () => {
          const { status } = await getUserState();
          if (status === "unauthenticated") return redirect("/");
          if (status === "dashboard") return redirect("/dashboard");
          return null;
        },
      },
      { path: "*", element: <NotFound /> },
      {
        path: "/register",
        element: <RegisterView />,
        loader: async () => {
          const { status } = await getUserState();
          if (status !== "unauthenticated") return redirect("/"); // logged-in users go home
          return null;
        },
      },
    ],
  },
]);

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);
