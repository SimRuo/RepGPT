import { Container, Typography, Box, TextField, Button, CircularProgress, Alert } from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { getLoggedInUserId } from "../services/auth";
import { generatePlan, createPlan } from "../services/WorkoutPlanService";
import { addPrompt } from "../services/PromptHistoryService";

function ChatView() {
  const [prompt, setPrompt] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const navigate = useNavigate();

  async function handleSend() {
    setLoading(true);
    setError(null);

    try {
      const userId = getLoggedInUserId();
      if (!userId) throw new Error("User not logged in");

      const planDto = await generatePlan(prompt);

      await createPlan(planDto, userId);

      await addPrompt(userId, prompt, JSON.stringify(planDto));

      navigate("/dashboard");
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

      {error && (
        <Alert severity="error" sx={{ mt: 2 }}>
          {error}
        </Alert>
      )}
    </Container>
  );
}

export default ChatView;
