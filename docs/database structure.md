# Fitness Tracker – Database Structure

## Core Tables & Relationships

### 1. User

Represents a person using the app.

- Each user:
  - Has one workout plan
  - Can log workouts they’ve completed
  - Can send prompts to the AI

**Fields:** `Id`, `Name`, `Email`, `PasswordHash`, etc.

---

### 2. WorkoutPlan

A user's structured weekly workout schedule.

- Each user has one workout plan.
- A plan is made up of multiple days.

**Fields:** `Id`, `UserId`, `Name`, `Goal`

---

### 3. WorkoutDay

Represents a specific day in the workout plan (e.g., "Monday", "Tuesday").

- Each WorkoutPlan has many WorkoutDays.
- Each WorkoutDay has many exercises.

**Fields:** `Id`, `WorkoutPlanId`, `DayOfWeek`, `Notes`

---

### 4. WorkoutExercise

Defines a specific exercise planned for a day.

- Each WorkoutDay has many WorkoutExercises.
- Each WorkoutExercise refers to one Exercise (e.g., "Bench Press").
- Includes planned reps, sets, weight, or time.

**Fields:** `Id`, `WorkoutDayId`, `ExerciseId`, `TargetSets`, `TargetReps`, `TargetWeight`, `TargetTime`

---

### 5. Exercise

A master list of exercises.

- Integrated with external data such as ExerciseDB.
- Used to describe what each WorkoutExercise refers to.

**Fields:** `Id`, `Name`, `Description`, `Equipment`, `MuscleGroup`, etc.

---

### 6. WorkoutLog

Tracks what the user actually performed during a session.

- Each log belongs to one User.
- Each log refers to a WorkoutExercise.
- Logs include actual metrics (weight, time, sets, reps) and date.

**Fields:** `Id`, `UserId`, `WorkoutExerciseId`, `Date`, `CompletedSets`, `CompletedReps`, `ActualWeight`, `ActualTime`, `Notes`

---

### 7. PromptHistory

Stores LLM interaction history.

- Each prompt is linked to one User.
- Stores both user input and LLM response.

**Fields:** `Id`, `UserId`, `PromptText`, `ResponseText`, `CreatedAt`, `PromptType`

---

## Relationship Summary

| Parent         | Child             | Relationship  |
|----------------|------------------|---------------|
| User           | WorkoutPlan       | One-to-One    |
| WorkoutPlan    | WorkoutDay        | One-to-Many   |
| WorkoutDay     | WorkoutExercise   | One-to-Many   |
| WorkoutExercise| WorkoutLog        | One-to-Many   |
| Exercise       | WorkoutExercise   | One-to-Many   |
| User           | WorkoutLog        | One-to-Many   |
| User           | PromptHistory     | One-to-Many   |

---

## Example Use Case

1. Anna signs up as a new user.
2. She enters a goal: *“I want to lose fat with bodyweight workouts.”*
3. That prompt is saved to `PromptHistory`.
4. The LLM replies with a weekly plan.
5. A `WorkoutPlan` is created:
   - Monday → Push-ups, Plank
   - Wednesday → Squats, Wall Sit
   - Friday → Burpees
6. Each of these exercises is stored as `WorkoutDay` and `WorkoutExercise` entries.
7. When Anna completes a workout, she logs it in `WorkoutLog`.

---

## Additional Assets Available

- ERD as a `.drawio` file
- SQL schema definition
