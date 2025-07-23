import { Paper, Box, Avatar, Typography } from "@mui/material";

function FeatureCard({ title, description, icon, color, theme }) {
  return (
    <Paper
      elevation={3}
      sx={{
        p: 4,
        height: "100%",
        borderRadius: 3,
        transition: "transform 0.3s, box-shadow 0.3s",
        "&:hover": {
          transform: "translateY(-8px)",
          boxShadow: theme.shadows[8],
        },
      }}
    >
      <Box sx={{ display: "flex", alignItems: "center", mb: 3 }}>
        <Avatar sx={{ bgcolor: color, mr: 2 }}>{icon}</Avatar>
        <Typography variant="h5" fontWeight={600}>
          {title}
        </Typography>
      </Box>
      <Typography>{description}</Typography>
    </Paper>
  );
}

export default FeatureCard;
