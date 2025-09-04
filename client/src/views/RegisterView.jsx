import { useState } from "react";
import { registerUser } from "../services/UserService";
import { loginAs } from "../services/auth";
import { Container, Box, TextField, Typography, Button, Alert, Paper, CircularProgress } from "@mui/material";
import { useNavigate } from "react-router-dom";

function RegisterView({ onRegisterSuccess }) {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false); 
  const navigate = useNavigate();

  async function handleSubmit(e) {
    e.preventDefault();
    setLoading(true);
    setError(null); 
    try {
      const user = await registerUser({ name, email, password });
      loginAs(user.id); 
      if (onRegisterSuccess) onRegisterSuccess(user);
      navigate("/");
    } catch (err) {
      setError(err?.response?.data?.error || err?.message || "Failed to register. Please try again.");
    } finally {
      setLoading(false);
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
          <Alert severity="error" sx={{ mb: 2 }}>
            {error}
          </Alert>
        )}

        <Box component="form" onSubmit={handleSubmit}>
          <TextField
            fullWidth
            label="Name"
            margin="normal"
            required
            value={name}
            onChange={(e) => setName(e.target.value)}
            disabled={loading}
          />
          <TextField
            fullWidth
            label="Email"
            type="email"
            margin="normal"
            required
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            disabled={loading} 
          />
          <TextField
            fullWidth
            label="Password"
            type="password"
            margin="normal"
            required
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            disabled={loading}
          />

          <Button fullWidth type="submit" variant="contained" sx={{ mt: 2 }} disabled={loading}>
            {loading ? <CircularProgress size={22} /> : "Register"} {}
          </Button>
        </Box>

        {loading  && (
          <Box sx={{ display: "flex", justifyContent: "center", mt: 2 }}>
            <CircularProgress />
          </Box>
        )}
      </Paper>
    </Container>
  );
}

export default RegisterView;
