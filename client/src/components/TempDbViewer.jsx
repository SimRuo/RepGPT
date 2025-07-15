import { useEffect, useState } from "react";
import { getExercises } from "../services/ExerciseService";
import { getPromptHistory } from "../services/PromptHistoryService";
import { getAllPlans } from "../services/WorkoutPlanService";
import { getWorkoutDays } from "../services/WorkoutDayService";
import { getWorkoutExercises } from "../services/WorkoutExerciseService";
import { getWorkoutLogs } from "../services/WorkoutLogService";
import { getAllUsers } from "../services/UserService";
import { loginUser, loginAs, getLoggedInUserId, logout } from "../services/auth";

function TempDbViewer() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loggedInUser, setLoggedInUser] = useState(null);

  const [allExercises, setAllExercises] = useState([]);
  const [allPromptHistories, setAllPromptHistories] = useState([]);
  const [allPlans, setAllPlans] = useState([]);
  const [allWorkoutDays, setAllWorkoutDays] = useState([]);
  const [allWorkoutExercises, setAllWorkoutExercises] = useState([]);
  const [allWorkoutLogs, setAllWorkoutLogs] = useState([]);
  const [allUsers, setAllUsers] = useState([]);

  useEffect(() => {
    async function fetchData() {
      const users = await getAllUsers();
      setAllUsers(users);

      setAllExercises(await getExercises());
      setAllPromptHistories(await getPromptHistory());
      setAllPlans(await getAllPlans());
      setAllWorkoutDays(await getWorkoutDays());
      setAllWorkoutExercises(await getWorkoutExercises());
      setAllWorkoutLogs(await getWorkoutLogs());

      const userId = getLoggedInUserId();
      if (userId) {
        const user = users.find((u) => u.id === userId);
        setLoggedInUser(user);
      }
    }
    fetchData();
  }, []);

  async function handleLogin(e) {
    e.preventDefault();
    try {
      const user = await loginUser(email, password);
      loginAs(user.id);
      setLoggedInUser(user);
    } catch (err) {
      alert(err);
    }
  }

  function handleLogout() {
    logout();
    setLoggedInUser(null);
  }

  return (
    <div style={{ padding: "1rem" }}>
      <h1>RepGPT Debug Viewer</h1>

      <section>
        <h2>Login</h2>
        {loggedInUser ? (
          <>
            <p>
              Logged in as <strong>{loggedInUser.name}</strong> ({loggedInUser.email})
            </p>
            <button onClick={handleLogout}>Log out</button>
          </>
        ) : (
          <form onSubmit={handleLogin}>
            <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} required />
            <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required />
            <button type="submit">Log in</button>
          </form>
        )}
      </section>

      <section>
        <h2>Exercises</h2>
        <ul>
          {allExercises.map((ex) => (
            <li key={ex.id}>
              {ex.name} — {ex.equipment}
            </li>
          ))}
        </ul>
      </section>

      <section>
        <h2>Prompt History</h2>
        <ul>
          {allPromptHistories.map((p) => (
            <li key={p.id}>
              {p.prompt} → <strong>{p.result.substring(0, 60)}...</strong>
            </li>
          ))}
        </ul>
      </section>

      <section>
        <h2>Workout Plans</h2>
        <ul>
          {allPlans.map((plan) => (
            <li key={plan.id}>
              {plan.name} ({plan.goal})
            </li>
          ))}
        </ul>
      </section>

      <section>
        <h2>Workout Days</h2>
        <ul>
          {allWorkoutDays.map((day) => (
            <li key={day.id}>
              {day.dayOfTheWeek} — {day.notes}
            </li>
          ))}
        </ul>
      </section>

      <section>
        <h2>Workout Exercises</h2>
        <ul>
          {allWorkoutExercises.map((ex) => (
            <li key={ex.id}>
              {ex.exerciseName || "Exercise ID: " + ex.exerciseId} — {ex.sets}x{ex.reps} @ {ex.targetWeight}kg ({ex.targetTime})
            </li>
          ))}
        </ul>
      </section>

      <section>
        <h2>Workout Logs</h2>
        <ul>
          {allWorkoutLogs.map((log) => (
            <li key={log.id}>
              {log.date}: {log.setsCompleted} sets, {log.repsCompleted} reps @ {log.weightUsed}kg
            </li>
          ))}
        </ul>
      </section>

      <section>
        <h2>Users</h2>
        <ul>
          {allUsers.map((user) => (
            <li key={user.id}>
              {user.email}: Welcome mister {user.name}!
            </li>
          ))}
        </ul>
      </section>
    </div>
  );
}

export default TempDbViewer;
