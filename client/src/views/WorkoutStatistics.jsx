import { useEffect, useState } from "react";
import { Container, Typography, Grid, Paper, CircularProgress, Alert, Box, FormControl, InputLabel, Select, MenuItem } from "@mui/material";
import { PieChart } from "@mui/x-charts/PieChart";
import { LineChart } from "@mui/x-charts/LineChart";
import { getOverallStats, getExerciseStats } from "../services/WorkoutStatisticsService";
import { getLoggedInUserId } from "../services/auth";

function WorkoutStatistics() {
  const [stats, setStats] = useState(null);
  const [selectedExercise, setSelectedExercise] = useState("");
  const [exerciseStats, setExerciseStats] = useState(null);
  const [exerciseLoading, setExerciseLoading] = useState(false);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // --- Fetch overall stats ---
  useEffect(() => {
    async function fetchStats() {
      try {
        const userId = getLoggedInUserId();
        if (!userId) throw new Error("User not logged in");
        const data = await getOverallStats(userId);
        setStats(data);
      } catch (err) {
        setError(err.message || "Failed to fetch workout statistics");
      } finally {
        setLoading(false);
      }
    }
    fetchStats();
  }, []);

  // --- Fetch exercise stats ---
  async function handleExerciseChange(exercise) {
    setSelectedExercise(exercise);
    if (!exercise) return;
    try {
      setExerciseLoading(true);
      const userId = getLoggedInUserId();
      const data = await getExerciseStats(userId, exercise);
      setExerciseStats(data);
    } catch (err) {
      setError(err.message || "Failed to fetch exercise stats");
    } finally {
      setExerciseLoading(false);
    }
  }

  if (loading) return <CircularProgress sx={{ mt: 4 }} />;
  if (error) return <Alert severity="error">{error}</Alert>;
  if (!stats || !stats.weeklyVolume?.length) {
    return (
      <Container maxWidth="sm" sx={{ mt: 8, textAlign: "center" }}>
        <Typography variant="h5" gutterBottom>
          No workout data yet
        </Typography>
        <Typography variant="body1" color="text.secondary">
          Finish a workout to see your statistics.
        </Typography>
      </Container>
    );
  }

  // Chart data
  const lineData = stats.weeklyVolume.map((w) => ({ x: w.week, y: w.avgDailyVolume }));
  const exercisePieData = stats.topExercises.map((e, index) => ({
    id: index,
    value: e.count,
    label: e.exercise,
  }));

  const summaryCards = [
    { label: "Average Daily Volume (this week)", value: `${stats.avgDailyVolumeThisWeek.toFixed(1)} kg` },
    { label: "Most Frequent Exercise", value: stats.topExercises?.[0]?.exercise || "N/A" },
    { label: "Workouts Logged", value: stats.workoutsLogged },
  ];

  return (
    <Container maxWidth="lg" sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom fontWeight={700}>
        Workout Statistics
      </Typography>

      <Grid container spacing={4}>
        {/* Summary Cards */}
        {summaryCards.map((card, i) => (
          <Grid item xs={12} md={4} key={i} sx={{ display: "flex" }}>
            <Paper
              sx={{
                p: 3,
                textAlign: "center",
                flex: 1,
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
              }}
            >
              <Typography variant="h6" gutterBottom>
                {card.label}
              </Typography>
              <Typography variant="h4">{card.value}</Typography>
            </Paper>
          </Grid>
        ))}

        {/* Top Exercises Pie Chart */}
        <Grid item xs={12} md={6} sx={{ display: "flex" }}>
          <Paper sx={{ p: 3, flex: 1 }}>
            <Typography variant="h6" gutterBottom>
              Top 5 Exercises
            </Typography>
            <Box sx={{ height: 300, width: "100%" }}>
              <PieChart series={[{ data: exercisePieData, innerRadius: 40, outerRadius: 100 }]} width={400} height={300} />
            </Box>
          </Paper>
        </Grid>

        {/* Volume Over Time Chart */}
        <Grid item xs={12} md={6} sx={{ display: "flex" }}>
          <Paper sx={{ p: 3, flex: 1 }}>
            <Typography variant="h6" gutterBottom>
              Avg Daily Volume (per week)
            </Typography>
            <Box sx={{ height: 300, width: "100%" }}>
              <LineChart
                xAxis={[{ scaleType: "band", data: lineData.map((d) => d.x), label: "Week" }]}
                series={[{ data: lineData.map((d) => d.y), label: "Avg Daily Volume (kg)" }]}
                width={400}
                height={300}
              />
            </Box>
          </Paper>
        </Grid>
      </Grid>

      {/* Exercise Dropdown & Graphs */}
      <Box sx={{ mt: 6 }}>
        <Typography variant="h5" gutterBottom>
          Detailed Exercise View
        </Typography>
        <FormControl variant="outlined" sx={{ minWidth: 250, mb: 3 }}>
          <InputLabel id="exercise-label">Select Exercise</InputLabel>
          <Select
            labelId="exercise-label"
            value={selectedExercise}
            onChange={(e) => handleExerciseChange(e.target.value)}
            label="Select Exercise"
          >
            <MenuItem value="">
              <em>None</em>
            </MenuItem>
            {stats.allExercises?.map((ex) => (
              <MenuItem key={ex} value={ex}>
                {ex}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        {selectedExercise && !exerciseLoading && exerciseStats && (
          <Grid container spacing={4}>
            <Grid item xs={12} md={6} sx={{ display: "flex" }}>
              <Paper sx={{ p: 3, flex: 1 }}>
                <Typography variant="h6" gutterBottom>
                  {selectedExercise} - Avg Daily Volume
                </Typography>
                <Box sx={{ height: 300, width: "100%" }}>
                  <LineChart
                    xAxis={[{ scaleType: "band", data: exerciseStats.weeklyVolume.map((d) => d.week), label: "Week" }]}
                    series={[{ data: exerciseStats.weeklyVolume.map((d) => d.avgDailyVolume), label: "Avg Daily Volume (kg)" }]}
                    width={400}
                    height={300}
                  />
                </Box>
              </Paper>
            </Grid>

            <Grid item xs={12} md={6} sx={{ display: "flex" }}>
              <Paper sx={{ p: 3, flex: 1 }}>
                <Typography variant="h6" gutterBottom>
                  {selectedExercise} - Avg Weight Per Rep
                </Typography>
                <Box sx={{ height: 300, width: "100%" }}>
                  <LineChart
                    xAxis={[{ scaleType: "band", data: exerciseStats.avgWeightPerRep.map((d) => d.week), label: "Week" }]}
                    series={[{ data: exerciseStats.avgWeightPerRep.map((d) => d.avgWeight), label: "Avg Weight (kg)" }]}
                    width={400}
                    height={300}
                  />
                </Box>
              </Paper>
            </Grid>
          </Grid>
        )}

        {selectedExercise && exerciseLoading && <CircularProgress sx={{ mt: 4 }} />}
      </Box>
    </Container>
  );
}

export default WorkoutStatistics;
