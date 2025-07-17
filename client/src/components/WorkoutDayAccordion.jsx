import { Accordion, AccordionSummary, AccordionDetails, Typography, Box, Avatar, Chip } from "@mui/material";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import WorkoutExerciseList from "./WorkoutExerciseList";

function WorkoutDayAccordion({ day }) {
  const date = new Date(day.dayOfTheWeek);
  const formattedDate = date.toLocaleDateString("en-US", {
    weekday: "long",
    month: "short",
    day: "numeric",
  });
  const exerciseCount = day.workoutExercises.length;

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
        <WorkoutExerciseList exercises={day.workoutExercises} />
      </AccordionDetails>
    </Accordion>
  );
}

export default WorkoutDayAccordion;
