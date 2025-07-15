import { useState } from "react";
import { loginUser, loginAs } from "../services/auth";
import { Container, Box, TextField, Typography, Button, Alert, Paper } from "@mui/material";
import { useNavigate } from "react-router-dom";

function LoginView({ onLoginSuccess }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  async function handleSubmit(e) {
    e.preventDefault();
    try {
      const user = await loginUser(email, password);
      loginAs(user.id);
      setError(null);
      if (onLoginSuccess) onLoginSuccess(user);
      navigate("/");
    } catch {
      setError("Invalid email or password");
    }
  }

  // I could add another loader here to check if a user is logged in and funnel them through to chat/dashboard
  
  return (
    <Container maxWidth="xs">
      <Paper elevation={3} sx={{ padding: 4, marginTop: 8 }}>
        <Typography variant="h5" align="center" gutterBottom>
          RepGPT Login
        </Typography>

        {error && (
          <Alert severity="error" sx={{ marginBottom: 2 }}>
            {error}
          </Alert>
        )}

        <Box component="form" onSubmit={handleSubmit}>
          <TextField
            fullWidth
            label="Email"
            type="email"
            margin="normal"
            required
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />

          <TextField
            fullWidth
            label="Password"
            type="password"
            margin="normal"
            required
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />

          <Button fullWidth type="submit" variant="contained" sx={{ marginTop: 2 }}>
            Log In
          </Button>
        </Box>
      </Paper>
    </Container>
  );
}

export default LoginView;
