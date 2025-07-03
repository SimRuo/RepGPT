# Fitness Tracker API Documentation

Base URL: `https://your-api-domain.com/api`

---

## Authentication

All endpoints require a valid JWT token in the `Authorization` header:

```
Authorization: Bearer {token}
```

---

## User

### `POST /users/register`

Register a new user.

**Body:**

```json
{
  "email": "user@example.com",
  "password": "yourPassword",
  "name": "Anna"
}
```

**Response:** `201 Created`

```json
{
  "userId": 1,
  "email": "user@example.com",
  "name": "Anna"
}
```

---

### `POST /users/login`

Authenticate user and get JWT token.

**Body:**

```json
{
  "email": "user@example.com",
  "password": "yourPassword"
}
```

**Response:** `200 OK`

```json
{
  "token": "eyJhbGciOi..."
}
```

---

## LLM Prompt Interaction

### `POST /llm/generate-plan`

Send a natural language prompt to generate a workout plan.

**Body:**

```json
{
  "userId": 1,
  "promptText": "I want a beginner workout I can do at home"
}
```

**Response:** `200 OK`

```json
{
  "planId": 12,
  "name": "Beginner Plan",
  "goal": "Fat loss",
  "days": [
    {
      "dayOfWeek": "Monday",
      "exercises": ["Pushups", "Bodyweight Squats", "Plank"]
    }
  ]
}
```

---

## Workout Plans

### `GET /workoutplans/{userId}`

Get the active workout plan for a user.

**Response:** `200 OK`

```json
{
  "planId": 12,
  "name": "Beginner Plan",
  "goal": "Fat loss",
  "days": [
    {
      "dayOfWeek": "Monday",
      "exercises": [
        { "exerciseId": 101, "name": "Pushups", "sets": 3, "reps": 10 }
      ]
    }
  ]
}
```

---

## Modify Plan

### `PUT /workoutexercises/{id}`

Update or replace a specific workout exercise.

**Body:**

```json
{
  "exerciseId": 105,
  "targetSets": 4,
  "targetReps": 12
}
```

**Response:** `204 No Content`

---

## Workout Logs

### `POST /workoutlogs`

Log a completed workout.

**Body:**

```json
{
  "userId": 1,
  "workoutExerciseId": 33,
  "date": "2025-06-28",
  "completedSets": 3,
  "completedReps": 10,
  "actualWeight": 0,
  "actualTime": 60,
  "notes": "Felt strong"
}
```

**Response:** `201 Created`

---

### `GET /workoutlogs/{userId}`

Get all logs for a user.

**Response:** `200 OK`

```json
[
  {
    "date": "2025-06-28",
    "exercise": "Plank",
    "actualTime": 60
  }
]
```

---

## Prompt History

### `GET /prompts/{userId}`

Get all prompts and responses submitted by a user.

**Response:** `200 OK`

```json
[
  {
    "promptText": "Make me a routine for home",
    "responseText": "Here's a 3-day full body routine...",
    "createdAt": "2025-06-28T14:01:00Z",
    "promptType": "initial_plan"
  }
]
```

---

## Status Codes Summary

- `200 OK` – Success
- `201 Created` – Resource successfully created
- `204 No Content` – Update/delete successful
- `400 Bad Request` – Invalid request data
- `401 Unauthorized` – Missing or invalid auth token
- `404 Not Found` – Resource not found
- `500 Internal Server Error` – Internal server error

