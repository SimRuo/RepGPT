import { Card, CardContent, Typography, Divider, Chip, Box } from "@mui/material";
import WorkoutDayAccordion from "./WorkoutDayAccordion";
import FitnessCenterIcon from "@mui/icons-material/FitnessCenter";

function WorkoutPlanCard({ plan }) {
  return (
    <Card
      sx={{
        height: "100%",
        display: "flex",
        flexDirection: "column",
        borderRadius: "12px",
        boxShadow: "0 8px 16px rgba(0,0,0,0.1)",
        transition: "transform 0.3s, box-shadow 0.3s",
        "&:hover": {
          boxShadow: "0 12px 20px rgba(0,0,0,0.15)",
        },
      }}
    >
      <CardContent sx={{ flexGrow: 1 }}>
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            mb: 1,
          }}
        >
          <FitnessCenterIcon color="primary" sx={{ mr: 1 }} />
          <Typography variant="h6" sx={{ fontWeight: 600 }}>
            {plan.name}
          </Typography>
        </Box>

        <Chip label={plan.goal} size="small" color="secondary" sx={{ mb: 2 }} />

        <Divider sx={{ my: 2 }} />

        {plan.workoutDays.map((day) => (
          <WorkoutDayAccordion key={day.id} day={day} />
        ))}
      </CardContent>
    </Card>
  );
}

export default WorkoutPlanCard;
