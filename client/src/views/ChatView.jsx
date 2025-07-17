import { Container, Typography, Box, TextField, Button, CircularProgress, Alert } from "@mui/material";
import { useState } from "react";
import { getLoggedInUserId } from "../services/auth";
import { generatePlan, createPlan } from "../services/WorkoutPlanService";

function ChatView() {
  const [prompt, setPrompt] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [successMessage, setSuccessMessage] = useState(null);

  async function handleSend() {
    setLoading(true);
    setError(null);
    setSuccessMessage(null);

    try {
      const userId = getLoggedInUserId();
      if (!userId) throw new Error("User not logged in");
      console.log("USER ID:", userId);

      // 1. Call GPT generate API through service
      const planDto = await generatePlan(prompt);
      console.log(prompt);
      console.log(planDto);
      // 2. Use returned plan to create it in DB
      // These 2 steps should maybe be just one call to the backend? Works well atleast
      const result = await createPlan(planDto, userId);

      setSuccessMessage(`Workout plan "${result.name}" created successfully!`);
      setPrompt("");
    } catch (err) {
      setError(err.response?.data?.error || err.message || "Something went wrong");
    } finally {
      setLoading(false);
    }
  }

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Typography variant="h5" gutterBottom>
        Generate Your Workout Plan
      </Typography>

      <TextField
        label="Describe your goal"
        fullWidth
        multiline
        minRows={3}
        value={prompt}
        onChange={(e) => setPrompt(e.target.value)}
        disabled={loading}
      />

      <Box sx={{ mt: 2 }}>
        <Button variant="contained" onClick={handleSend} disabled={!prompt || loading}>
          Generate
        </Button>
      </Box>

      {loading && (
        <Box sx={{ mt: 2 }}>
          <CircularProgress />
        </Box>
      )}

      {successMessage && (
        <Alert severity="success" sx={{ mt: 2 }}>
          {successMessage}
        </Alert>
      )}

      {error && (
        <Alert severity="error" sx={{ mt: 2 }}>
          {error}
        </Alert>
      )}
    </Container>
  );
}

export default ChatView;
