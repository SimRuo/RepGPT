import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Box, Button, Typography, Paper, CircularProgress } from "@mui/material";
import { deletePlan, getPlansForUser } from "../services/WorkoutPlanService";
import { deleteUser } from "../services/UserService";
import { getLoggedInUserId } from "../services/auth";

function SettingsView() {
  const navigate = useNavigate();
  const [planId, setPlanId] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function fetchPlan() {
      try {
        const userId = getLoggedInUserId();
        const plans = await getPlansForUser(userId);
        if (plans.length > 0) {
          setPlanId(plans[0].id); // assuming only one plan per user
        }
      } catch (error) {
        console.error("Failed to fetch plan:", error);
      } finally {
        setLoading(false);
      }
    }
    fetchPlan();
  }, []);

  async function handleDeletePlan() {
    if (!planId) return;

    const confirmed = window.confirm("Are you sure you want to delete your workout plan?");
    if (!confirmed) return;

    try {
      await deletePlan(planId);
      navigate("/chat");
    } catch (error) {
      console.error("Failed to delete workout plan", error);
    }
  }

  async function handleDeleteAccount() {
    const confirmed = window.confirm("Are you sure you want to permanently delete your account? This action cannot be undone.");
    if (!confirmed) return;

    try {
      const userId = getLoggedInUserId();
      await deleteUser(userId);
      localStorage.clear();
      navigate("/");
    } catch (error) {
      console.error("Failed to delete account", error);
    }
  }

  if (loading) {
    return (
      <Box sx={{ display: "flex", justifyContent: "center", mt: 8 }}>
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box sx={{ maxWidth: 500, mx: "auto", mt: 6, p: 3 }}>
      <Paper sx={{ p: 3, textAlign: "center" }}>
        <Typography variant="h5" gutterBottom>
          Settings
        </Typography>
        <Typography variant="body2" color="text.secondary" sx={{ mb: 3 }}>
          Manage your workout data and account.
        </Typography>

        <Button variant="contained" color="warning" fullWidth sx={{ mb: 2 }} onClick={handleDeletePlan} disabled={!planId}>
          Delete Workout Plan
        </Button>

        <Button variant="contained" color="error" fullWidth onClick={handleDeleteAccount}>
          Delete Account
        </Button>
      </Paper>
    </Box>
  );
}

export default SettingsView;
