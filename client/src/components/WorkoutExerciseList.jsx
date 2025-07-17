import { List, ListItem, ListItemText, ListItemAvatar, Avatar, Divider, Box, Typography, Chip } from "@mui/material";
import FitnessCenterIcon from "@mui/icons-material/FitnessCenter";

function WorkoutExerciseList({ exercises }) {
  return (
    <List dense sx={{ pt: 0 }}>
      {exercises.map((ex, index) => (
        <Box key={ex.id}>
          <ListItem
            sx={{
              px: 0,
              py: 1,
              alignItems: "flex-start",
            }}
          >
            <ListItemAvatar sx={{ minWidth: 40 }}>
              <Avatar
                sx={{
                  bgcolor: "rgba(0, 120, 212, 0.1)",
                  width: 28,
                  height: 28,
                }}
              >
                <FitnessCenterIcon color="primary" sx={{ fontSize: "16px" }} />
              </Avatar>
            </ListItemAvatar>
            <ListItemText
              primary={
                <Typography variant="subtitle2" sx={{ fontWeight: 600 }}>
                  {ex.exerciseName}
                </Typography>
              }
              secondary={
                <Box
                  component="span"
                  sx={{
                    display: "flex",
                    flexWrap: "wrap",
                    gap: "8px",
                    mt: 0.5,
                  }}
                >
                  <Chip label={`${ex.sets} sets`} size="small" variant="outlined" />
                  <Chip label={`${ex.reps} reps`} size="small" variant="outlined" />
                  {ex.targetWeight !== null && ex.targetWeight !== undefined && (
                    <Chip label={`${ex.targetWeight} kg`} size="small" variant="outlined" />
                  )}{" "}
                  {ex.targetTime && <Chip label={`${ex.targetTime}s`} size="small" variant="outlined" />}
                </Box>
              }
            />
          </ListItem>
          {index < exercises.length - 1 && <Divider component="li" sx={{ my: 0.5 }} />}
        </Box>
      ))}
    </List>
  );
}

export default WorkoutExerciseList;
