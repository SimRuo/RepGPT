// WorkoutStatistics.jsx (responsive)
import { useEffect, useState } from "react";
import {
  Container,
  Typography,
  Grid,
  Paper,
  CircularProgress,
  Alert,
  Box,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  useMediaQuery
} from "@mui/material";
import { useTheme } from "@mui/material/styles";
import { PieChart } from "@mui/x-charts/PieChart";
import { LineChart } from "@mui/x-charts/LineChart";
import { getOverallStats, getExerciseStats } from "../services/WorkoutStatisticsService";
import { getLoggedInUserId } from "../services/auth";

function WorkoutStatistics() {
  const theme = useTheme();
  const downSm = useMediaQuery(theme.breakpoints.down("sm"));
  const downMd = useMediaQuery(theme.breakpoints.down("md"));

  // Chart sizes responsive
  const chartHeight = downSm ? 220 : downMd ? 260 : 320;
  const chartWidth  = downSm ? 280 : downMd ? 360 : 520;

  const [stats, setStats] = useState(null);
  const [selectedExercise, setSelectedExercise] = useState("");
  const [exerciseStats, setExerciseStats] = useState(null);
  const [exerciseLoading, setExerciseLoading] = useState(false);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

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

  if (loading) return <Box sx={{ display: "flex", justifyContent: "center", mt: 4 }}><CircularProgress /></Box>;
  if (error) return <Alert severity="error">{error}</Alert>;
  if (!stats || !stats.weeklyVolume?.length) {
    return (
      <Container maxWidth="sm" sx={{ mt: 6, textAlign: "center" }}>
        <Typography variant={downSm ? "h6" : "h5"} gutterBottom>
          No workout data yet
        </Typography>
        <Typography variant="body2" color="text.secondary">
          Finish a workout to see your statistics.
        </Typography>
      </Container>
    );
  }

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
    <Container maxWidth="lg" sx={{ mt: downSm ? 2 : 4, px: { xs: 1.5, sm: 2 } }}>
      <Typography variant={downSm ? "h5" : "h4"} gutterBottom fontWeight={700}>
        Workout Statistics
      </Typography>

      <Grid container spacing={{ xs: 2, sm: 3, md: 4 }}>
        {/* Summary Cards */}
        {summaryCards.map((card, i) => (
          <Grid item xs={12} sm={6} md={4} key={i} sx={{ display: "flex" }}>
            <Paper
              elevation={1}
              sx={{
                p: { xs: 2, md: 3 },
                textAlign: "center",
                flex: 1,
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
                gap: 0.5,
              }}
            >
              <Typography variant={downSm ? "subtitle1" : "h6"} gutterBottom>
                {card.label}
              </Typography>
              <Typography variant={downSm ? "h5" : "h4"}>{card.value}</Typography>
            </Paper>
          </Grid>
        ))}

        {/* Top Exercises Pie Chart */}
        <Grid item xs={12} md={6} sx={{ display: "flex" }}>
          <Paper sx={{ p: { xs: 2, md: 3 }, flex: 1 }}>
            <Typography variant="h6" gutterBottom>
              Top 5 Exercises
            </Typography>
            <Box sx={{ display: "flex", justifyContent: "center", width: "100%" }}>
              <PieChart
                series={[{ data: exercisePieData, innerRadius: 40, outerRadius: downSm ? 90 : 110 }]}
                width={chartWidth}
                height={chartHeight}
              />
            </Box>
          </Paper>
        </Grid>

        {/* Volume Over Time Chart */}
        <Grid item xs={12} md={6} sx={{ display: "flex" }}>
          <Paper sx={{ p: { xs: 2, md: 3 }, flex: 1 }}>
            <Typography variant="h6" gutterBottom>
              Avg Daily Volume (per week)
            </Typography>
            <Box sx={{ display: "flex", justifyContent: "center", width: "100%" }}>
              <LineChart
                xAxis={[{ scaleType: "band", data: lineData.map((d) => d.x), label: downSm ? "" : "Week" }]}
                series={[{ data: lineData.map((d) => d.y), label: "Avg Daily Volume (kg)" }]}
                width={chartWidth}
                height={chartHeight}
              />
            </Box>
          </Paper>
        </Grid>
      </Grid>

      {/* Exercise Dropdown & Graphs */}
      <Box sx={{ mt: { xs: 4, md: 6 } }}>
        <Typography variant={downSm ? "h6" : "h5"} gutterBottom>
          Detailed Exercise View
        </Typography>

        <FormControl variant="outlined" size={downSm ? "small" : "medium"} fullWidth sx={{ maxWidth: 420, mb: 3 }}>
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
          <Grid container spacing={{ xs: 2, sm: 3, md: 4 }}>
            <Grid item xs={12} md={6} sx={{ display: "flex" }}>
              <Paper sx={{ p: { xs: 2, md: 3 }, flex: 1 }}>
                <Typography variant="h6" gutterBottom noWrap={!downSm}>
                  {selectedExercise} — Avg Daily Volume
                </Typography>
                <Box sx={{ display: "flex", justifyContent: "center", width: "100%" }}>
                  <LineChart
                    xAxis={[{ scaleType: "band", data: exerciseStats.weeklyVolume.map((d) => d.week), label: downSm ? "" : "Week" }]}
                    series={[{ data: exerciseStats.weeklyVolume.map((d) => d.avgDailyVolume), label: "Avg Daily Volume (kg)" }]}
                    width={chartWidth}
                    height={chartHeight}
                  />
                </Box>
              </Paper>
            </Grid>

            <Grid item xs={12} md={6} sx={{ display: "flex" }}>
              <Paper sx={{ p: { xs: 2, md: 3 }, flex: 1 }}>
                <Typography variant="h6" gutterBottom noWrap={!downSm}>
                  {selectedExercise} — Avg Weight Per Rep
                </Typography>
                <Box sx={{ display: "flex", justifyContent: "center", width: "100%" }}>
                  <LineChart
                    xAxis={[{ scaleType: "band", data: exerciseStats.avgWeightPerRep.map((d) => d.week), label: downSm ? "" : "Week" }]}
                    series={[{ data: exerciseStats.avgWeightPerRep.map((d) => d.avgWeight), label: "Avg Weight (kg)" }]}
                    width={chartWidth}
                    height={chartHeight}
                  />
                </Box>
              </Paper>
            </Grid>
          </Grid>
        )}

        {selectedExercise && exerciseLoading && (
          <Box sx={{ display: "flex", justifyContent: "center", mt: 3 }}>
            <CircularProgress />
          </Box>
        )}
      </Box>
    </Container>
  );
}

export default WorkoutStatistics;
