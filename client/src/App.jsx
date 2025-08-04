import "./App.css";
import { useState } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import {
  AppBar,
  Toolbar,
  Typography,
  IconButton,
  Drawer,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Box,
  CssBaseline,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import FitnessCenterIcon from "@mui/icons-material/FitnessCenter";
import DashboardIcon from "@mui/icons-material/Dashboard";
import HomeIcon from "@mui/icons-material/Home";
import SettingsIcon from "@mui/icons-material/Settings";
import LogoutIcon from "@mui/icons-material/Logout";
import LoginIcon from "@mui/icons-material/Login";
import PersonAddIcon from "@mui/icons-material/PersonAdd";
import { logout, getLoggedInUserId } from "./services/auth"; // <-- use your logout

function App() {
  const [drawerOpen, setDrawerOpen] = useState(false);
  const navigate = useNavigate();

  const toggleDrawer = (open) => () => {
    setDrawerOpen(open);
  };

  const handleNavigation = (path) => {
    navigate(path);
    setDrawerOpen(false);
  };

  const handleLogout = () => {
    logout(); // <-- clears userId
    navigate("/"); // redirect home or login page
    setDrawerOpen(false); // close sidebar
  };

  const isLoggedIn = Boolean(getLoggedInUserId()); // <-- now checks GUID presence

  return (
    <>
      <CssBaseline />
      {/* TopBar */}
      <AppBar position="static">
        <Toolbar>
          <IconButton edge="start" color="inherit" onClick={toggleDrawer(true)}>
            <MenuIcon />
          </IconButton>
          <FitnessCenterIcon sx={{ ml: 1, mr: 1 }} />
          <Typography
            sx={{
              fontFamily: "Leckerli One, arial",
              fontSize: { xs: "1.4rem", sm: "1.6rem", md: "1.8rem" },
            }}
          >
            RepGPT
          </Typography>
        </Toolbar>
      </AppBar>

      {/* Drawer */}
      <Drawer anchor="left" open={drawerOpen} onClose={toggleDrawer(false)}>
        <Box sx={{ width: 250 }} role="presentation">
          <List>
            {!isLoggedIn ? (
              <>
                <ListItem button onClick={() => handleNavigation("/login")}>
                  <ListItemIcon>
                    <LoginIcon />
                  </ListItemIcon>
                  <ListItemText primary="Login" />
                </ListItem>
                <ListItem button onClick={() => handleNavigation("/register")}>
                  <ListItemIcon>
                    <PersonAddIcon />
                  </ListItemIcon>
                  <ListItemText primary="Register" />
                </ListItem>
              </>
            ) : (
              <>
                <ListItem button onClick={() => handleNavigation("/dashboard")}>
                  <ListItemIcon>
                    <HomeIcon />
                  </ListItemIcon>
                  <ListItemText primary="Home" />
                </ListItem>
                <ListItem button onClick={() => handleNavigation("/statistics")}>
                  <ListItemIcon>
                    <DashboardIcon />
                  </ListItemIcon>
                  <ListItemText primary="Statistics" />
                </ListItem>
                <ListItem button onClick={() => handleNavigation("/settings")}>
                  <ListItemIcon>
                    <SettingsIcon />
                  </ListItemIcon>
                  <ListItemText primary="Settings" />
                </ListItem>
                <ListItem button onClick={handleLogout} sx={{ color: "red" }}>
                  <ListItemIcon>
                    <LogoutIcon sx={{ color: "red" }} />
                  </ListItemIcon>
                  <ListItemText primary="Logout" />
                </ListItem>
              </>
            )}
          </List>
        </Box>
      </Drawer>

      {/* Page Content */}
      <Outlet />
    </>
  );
}

export default App;
