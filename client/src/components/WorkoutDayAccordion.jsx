import { Accordion, AccordionSummary, AccordionDetails, Typography, Box, Avatar, Chip, Button, Grid, TextField } from "@mui/material";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import WorkoutExerciseList from "./WorkoutExerciseList";
import { createWorkoutLog } from "../services/WorkoutLogService";
import { useState } from "react";
import { getLoggedInUserId } from "../services/auth";

function WorkoutDayAccordion({ day }) {
  const [logging, setLogging] = useState(false);
  const [inputs, setInputs] = useState({});
  const date = new Date(day.dayOfTheWeek);
  const formattedDate = date.toLocaleDateString("en-US", {
    weekday: "long",
    month: "short",
    day: "numeric",
  });
  const exerciseCount = day.workoutExercises.length;

  const handleStart = () => {
    setLogging(true);
    const defaultInputs = {};
    day.workoutExercises.forEach((ex) => {
      defaultInputs[ex.id] = {
        actualSets: ex.sets,
        actualReps: ex.reps,
        actualWeight: ex.targetWeight,
        actualTime: ex.targetTime,
      };
    });
    setInputs(defaultInputs);
  };

  const handleChange = (id, field, value) => {
    setInputs((prev) => ({
      ...prev,
      [id]: { ...prev[id], [field]: value },
    }));
  };

  const handleSubmit = async () => {
    try {
      const userId = getLoggedInUserId();

      for (const ex of day.workoutExercises) {
        const input = inputs[ex.id];
        await createWorkoutLog({
          workoutExerciseId: ex.id,
          actualSets: parseInt(input.actualSets),
          actualReps: parseInt(input.actualReps),
          actualWeight: parseFloat(input.actualWeight),
          actualTime: input.actualTime,
          userId: userId,
        });
      }

      setLogging(false);
      alert("Workout logged!");
    } catch (err) {
      console.error(err);
      alert("Failed to log workout.");
    }
  };

  return (
    <Accordion
      sx={{
        mb: 1,
        borderRadius: "8px !important",
        overflow: "hidden",
        "&:before": {
          display: "none",
        },
      }}
    >
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        sx={{
          backgroundColor: "rgba(0, 120, 212, 0.05)",
          "&:hover": {
            backgroundColor: "rgba(0, 120, 212, 0.08)",
          },
        }}
      >
        <Box sx={{ display: "flex", alignItems: "center", width: "100%" }}>
          <Avatar
            sx={{
              bgcolor: "primary.main",
              width: 32,
              height: 32,
              mr: 2,
              fontSize: "0.875rem",
            }}
          >
            {date.getDate()}
          </Avatar>
          <Box sx={{ flexGrow: 1 }}>
            <Typography sx={{ fontWeight: 500 }}>{formattedDate}</Typography>
            {day.notes && (
              <Typography variant="body2" color="text.secondary" noWrap>
                {day.notes}
              </Typography>
            )}
          </Box>
          <Chip label={`${exerciseCount} ${exerciseCount === 1 ? "exercise" : "exercises"}`} size="small" />
        </Box>
      </AccordionSummary>
      <AccordionDetails sx={{ pt: 0, pb: 2 }}>
        {!logging ? (
          <>
            <WorkoutExerciseList exercises={day.workoutExercises} />
            <Button onClick={handleStart} variant="contained" sx={{ mt: 2 }}>
              Begin Workout
            </Button>
          </>
        ) : (
          <Box component="form" noValidate autoComplete="off">
            <Grid container spacing={2}>
              {day.workoutExercises.map((ex) => (
                <Grid item xs={12} key={ex.id}>
                  <Typography variant="subtitle1" fontWeight={600}>
                    {ex.exerciseName}
                  </Typography>
                  <Grid container spacing={1} sx={{ mt: 1 }}>
                    <Grid item xs={3}>
                      <TextField
                        label="Sets"
                        type="number"
                        value={inputs[ex.id]?.actualSets || ""}
                        onChange={(e) => handleChange(ex.id, "actualSets", e.target.value)}
                        size="small"
                      />
                    </Grid>
                    <Grid item xs={3}>
                      <TextField
                        label="Reps"
                        type="number"
                        value={inputs[ex.id]?.actualReps || ""}
                        onChange={(e) => handleChange(ex.id, "actualReps", e.target.value)}
                        size="small"
                      />
                    </Grid>
                    <Grid item xs={3}>
                      <TextField
                        label="Weight (kg)"
                        type="number"
                        value={inputs[ex.id]?.actualWeight || ""}
                        onChange={(e) => handleChange(ex.id, "actualWeight", e.target.value)}
                        size="small"
                      />
                    </Grid>
                    <Grid item xs={3}>
                      <TextField
                        label="Time (hh:mm:ss)"
                        type="text"
                        value={inputs[ex.id]?.actualTime || ""}
                        onChange={(e) => handleChange(ex.id, "actualTime", e.target.value)}
                        size="small"
                      />
                    </Grid>
                  </Grid>
                </Grid>
              ))}
            </Grid>
            <Button variant="contained" color="primary" onClick={handleSubmit} sx={{ mt: 3 }}>
              Submit Workout
            </Button>
          </Box>
        )}
      </AccordionDetails>
    </Accordion>
  );
}

export default WorkoutDayAccordion;
