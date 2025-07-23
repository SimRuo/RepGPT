import { useState } from "react";
import { registerUser } from "../services/UserService";
import { loginAs } from "../services/auth";
import { Container, Box, TextField, Typography, Button, Alert, Paper } from "@mui/material";
import { useNavigate } from "react-router-dom";

function RegisterView({ onRegisterSuccess }) {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  async function handleSubmit(e) {
    e.preventDefault();
    try {
      const user = await registerUser({ name, email, password });
      loginAs(user.id); // auto-login
      setError(null);
      if (onRegisterSuccess) onRegisterSuccess(user);
      navigate("/"); // redirect to home
    } catch (err) {
      setError(err.message || "Failed to register. Please try again.");
    }
  }

  return (
    <Container maxWidth="xs">
      <Paper elevation={3} sx={{ padding: 4, marginTop: 8 }}>
        <Typography variant="h5" align="center" gutterBottom>
          Create Your Account
        </Typography>
        <Typography align="center" gutterBottom>
          ** There is no email verification, feel free to use a fake email **
        </Typography>

        {error && (
          <Alert severity="error" sx={{ marginBottom: 2 }}>
            {error}
          </Alert>
        )}

        <Box component="form" onSubmit={handleSubmit}>
          <TextField fullWidth label="Name" margin="normal" required value={name} onChange={(e) => setName(e.target.value)} />
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
            Register
          </Button>
        </Box>
      </Paper>
    </Container>
  );
}

export default RegisterView;
