import { useEffect, useState } from "react";
import { Container, Typography, CircularProgress, Alert, Grid, Paper, Box } from "@mui/material";
import { applyProgressiveOverload, getPlansForUser } from "../services/WorkoutPlanService";
import { getWorkoutLogsForUser } from "../services/WorkoutLogService";
import { getLoggedInUserId } from "../services/auth";
import WorkoutPlanCard from "../components/WorkoutPlanCard";

function Dashboard() {
  const [plans, setPlans] = useState([]);
  const [logs, setLogs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function fetchData() {
      try {
        const userId = getLoggedInUserId();
        if (!userId) throw new Error("User not logged in");

        const [planData, logData] = await Promise.all([getPlansForUser(userId), getWorkoutLogsForUser(userId)]);

        setPlans(planData);
        setLogs(logData);
      } catch (err) {
        setError(err.message || "Failed to load plans or logs");
      } finally {
        setLoading(false);
      }
    }
    fetchData();
  }, []);

  async function handleProgressiveOverload(planId) {
    try {
      await applyProgressiveOverload(planId);
      const userId = getLoggedInUserId();
      const updatedPlans = await getPlansForUser(userId);
      setPlans(updatedPlans);
    } catch (err) {
      setError("Failed to apply progressive overload.");
    }
  }

  return (
    <Container maxWidth="xl" sx={{ mt: 4, mb: 6, minHeight: "80vh" }}>
      <Paper
        elevation={0}
        sx={{
          p: 3,
          mb: 4,
          background: "linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%)",
          borderRadius: "12px",
        }}
      >
        <Typography variant="h4" gutterBottom sx={{ fontWeight: 700, color: "primary.main" }}>
          Your Workout Plans
        </Typography>
        <Typography variant="subtitle1" color="text.secondary">
          Track your weekly progress and stay motivated
        </Typography>
      </Paper>

      {loading && (
        <Box sx={{ display: "flex", justifyContent: "center", mt: 8 }}>
          <CircularProgress size={60} thickness={4} />
        </Box>
      )}
      {error && (
        <Alert severity="error" sx={{ mb: 3 }}>
          {error}
        </Alert>
      )}

      {plans.map((plan) => (
        <Grid key={plan.id}>
          <WorkoutPlanCard plan={plan} logs={logs} setLogs={setLogs} onNextWeek={() => handleProgressiveOverload(plan.id)} />
        </Grid>
      ))}
    </Container>
  );
}

export default Dashboard;
