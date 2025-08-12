import {
  Container,
  Typography,
  Box,
  Paper,
  Button,
  Grid,
  List,
  ListItem,
  ListItemIcon,
  Avatar,
  useTheme,
  useMediaQuery,
} from "@mui/material";
import { TrendingUp, ShowChart, AccessTime, Person, Group, CheckCircle, SportsGymnastics } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import FeatureCard from "../components/FeatureCard";

function FrontPage() {
  const navigate = useNavigate();
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("md"));

  return (
    <Box>
      {/* Hero Section */}
      <Box
        sx={{
          pt: 8,
          pb: 12,
          background: `linear-gradient(135deg, ${theme.palette.primary.light} 0%, ${theme.palette.primary.dark} 100%)`,
          color: theme.palette.primary.contrastText,
          position: "relative",
          overflow: "hidden",
          "&:after": {
            content: '""',
            position: "absolute",
            bottom: "-2px",
            left: 0,
            right: 0,
            height: "100px",
            background: theme.palette.background.default,
            clipPath: "polygon(0 100%, 100% 100%, 100% 0)",
          },
        }}
      >
        <Container maxWidth="lg">
          <Grid container spacing={6} alignItems="center">
            <Grid item xs={12} md={6}>
              <Typography
                variant="h2"
                component="h1"
                gutterBottom
                sx={{
                  fontWeight: 800,
                  lineHeight: 1.2,
                  mb: 3,
                }}
              >
                Transform Your Fitness with AI
              </Typography>
              <Typography variant="h5" sx={{ mb: 4, opacity: 0.9 }}>
                RepGPT delivers personalized workout plans, intelligent tracking, and data-driven insights to maximize your gains.
              </Typography>
              <Box sx={{ display: "flex", gap: 2, mt: 4 }}>
                <Button
                  variant="contained"
                  size="large"
                  onClick={() => navigate("/register")}
                  sx={{
                    px: 4,
                    py: 1.5,
                    fontSize: "1.1rem",
                    fontWeight: 600,
                  }}
                >
                  Get Started Free
                </Button>
                <Button
                  variant="outlined"
                  color="inherit"
                  size="large"
                  onClick={() => navigate("/login")}
                  sx={{
                    px: 4,
                    py: 1.5,
                    fontSize: "1.1rem",
                    fontWeight: 600,
                  }}
                >
                  Log In
                </Button>
              </Box>
            </Grid>
          </Grid>
        </Container>
      </Box>

      {/* Features Section */}
      <Container maxWidth="lg" sx={{ py: 8, position: "relative", zIndex: 1 }}>
        <Grid container spacing={6}>
          <Grid item xs={12} md={4}>
            <FeatureCard
              title="Progressive Overload"
              description="Our AI automatically adjusts your workout intensity based on your performance, ensuring continuous improvement without plateaus."
              icon={<TrendingUp fontSize="large" />}
              color={theme.palette.secondary.main}
              theme={theme}
            />
          </Grid>
          <Grid item xs={12} md={4}>
            <FeatureCard
              title="Detailed Analytics"
              description="Visualize your progress with comprehensive charts and receive actionable insights to optimize your training."
              icon={<ShowChart fontSize="large" />}
              color={theme.palette.info.main}
              theme={theme}
            />
          </Grid>
          <Grid item xs={12} md={4}>
            <FeatureCard
              title="Time Efficient"
              description="Spend less time planning and more time training with optimized workouts tailored to your schedule and goals."
              icon={<AccessTime fontSize="large" />}
              color={theme.palette.success.main}
              theme={theme}
            />
          </Grid>
        </Grid>
      </Container>

      {/* How It Works Section */}
      <Box sx={{ py: 8, backgroundColor: theme.palette.grey[100] }}>
        <Container maxWidth="lg">
          <Typography variant="h3" align="center" sx={{ mb: 8, fontWeight: 700 }}>
            How RepGPT Works
          </Typography>

          <Grid container spacing={6} alignItems="center">
            <Grid item xs={12} md={6} order={isMobile ? 2 : 1}>
              <List>
                <ListItem sx={{ mb: 3 }}>
                  <ListItemIcon>
                    <Avatar sx={{ bgcolor: theme.palette.primary.main }}>
                      <Person fontSize="small" />
                    </Avatar>
                  </ListItemIcon>
                  <Box>
                    <Typography variant="h6" fontWeight={600}>
                      1. Tell Us About Yourself
                    </Typography>
                    <Typography>Enter your goals and preferences, leave the rest to the power of AI.</Typography>
                  </Box>
                </ListItem>

                <ListItem sx={{ mb: 3 }}>
                  <ListItemIcon>
                    <Avatar sx={{ bgcolor: theme.palette.secondary.main }}>
                      <SportsGymnastics fontSize="small" />
                    </Avatar>
                  </ListItemIcon>
                  <Box>
                    <Typography variant="h6" fontWeight={600}>
                      2. Get Your Custom Plan
                    </Typography>
                    <Typography>Receive a personalized workout plan generated by our AI based on your inputs.</Typography>
                  </Box>
                </ListItem>

                <ListItem>
                  <ListItemIcon>
                    <Avatar sx={{ bgcolor: theme.palette.success.main }}>
                      <CheckCircle fontSize="small" />
                    </Avatar>
                  </ListItemIcon>
                  <Box>
                    <Typography variant="h6" fontWeight={600}>
                      3. Track & Improve
                    </Typography>
                    <Typography>Log your workouts, track progress, and watch as the AI adjusts your plan for optimal results.</Typography>
                  </Box>
                </ListItem>
              </List>
            </Grid>
          </Grid>
        </Container>
      </Box>

      {/* CTA Section */}
      <Box
        sx={{
          py: 10,
          background: `linear-gradient(135deg, ${theme.palette.primary.light} 0%, ${theme.palette.primary.dark} 100%)`,
          color: theme.palette.secondary.contrastText,
        }}
      >
        <Container maxWidth="md" sx={{ textAlign: "center" }}>
          <Typography variant="h3" sx={{ mb: 3, fontWeight: 800 }}>
            Ready to Transform Your Workouts?
          </Typography>
          <Button
            variant="contained"
            color="primary"
            size="large"
            onClick={() => navigate("/register")}
            sx={{
              px: 6,
              py: 2,
              fontSize: "1.2rem",
              fontWeight: 600,
              boxShadow: theme.shadows[6],
            }}
          >
            Create your free account
          </Button>
        </Container>
      </Box>
    </Box>
  );
}

export default FrontPage;
