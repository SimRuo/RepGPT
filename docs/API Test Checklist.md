# 🧪 API Regression Test Checklist

This document is used to track manual testing of all REST endpoints via Postman.

## ✅ Legend

- ✅ = Test Passed
- ❌ = Test Failed
- 🕗 = Not Yet Tested

---

## 🧍‍♂️ UserController

| Endpoint          | Method | Status | Notes |
| ----------------- | ------ | ------ | ----- |
| `/api/Users`      | GET    | 🕗     |       |
| `/api/Users/{id}` | GET    | 🕗     |       |
| `/api/Users`      | POST   | 🕗     |       |
| `/api/Users/{id}` | PUT    | 🕗     |       |
| `/api/Users/{id}` | DELETE | 🕗     |       |

---

## 🏋️ WorkoutPlanController

| Endpoint                          | Method | Status | Notes              |
| --------------------------------- | ------ | ------ | ------------------ |
| `/api/WorkoutPlans`               | GET    | 🕗     |                    |
| `/api/WorkoutPlans/{id}`          | GET    | 🕗     |                    |
| `/api/WorkoutPlans`               | POST   | 🕗     | Generates from GPT |
| `/api/WorkoutPlans/{id}`          | PUT    | 🕗     |                    |
| `/api/WorkoutPlans/{id}`          | DELETE | 🕗     |                    |
| `/api/WorkoutPlans/generate-only` | POST   | 🕗     | Raw GPT response   |

---

## 📆 WorkoutDayController

| Endpoint                | Method | Status | Notes |
| ----------------------- | ------ | ------ | ----- |
| `/api/WorkoutDays`      | GET    | 🕗     |       |
| `/api/WorkoutDays/{id}` | GET    | 🕗     |       |
| `/api/WorkoutDays`      | POST   | 🕗     |       |
| `/api/WorkoutDays/{id}` | PUT    | 🕗     |       |
| `/api/WorkoutDays/{id}` | DELETE | 🕗     |       |

---

## 🏃 WorkoutExerciseController

| Endpoint                     | Method | Status | Notes |
| ---------------------------- | ------ | ------ | ----- |
| `/api/WorkoutExercises`      | GET    | 🕗     |       |
| `/api/WorkoutExercises/{id}` | GET    | 🕗     |       |
| `/api/WorkoutExercises`      | POST   | 🕗     |       |
| `/api/WorkoutExercises/{id}` | PUT    | 🕗     |       |
| `/api/WorkoutExercises/{id}` | DELETE | 🕗     |       |

---

## 📓 WorkoutLogController

| Endpoint                | Method | Status | Notes |
| ----------------------- | ------ | ------ | ----- |
| `/api/WorkoutLogs`      | GET    | 🕗     |       |
| `/api/WorkoutLogs/{id}` | GET    | 🕗     |       |
| `/api/WorkoutLogs`      | POST   | 🕗     |       |
| `/api/WorkoutLogs/{id}` | PUT    | 🕗     |       |
| `/api/WorkoutLogs/{id}` | DELETE | 🕗     |       |

---

## 📖 PromptHistoryController

| Endpoint                    | Method | Status | Notes |
| --------------------------- | ------ | ------ | ----- |
| `/api/PromptHistories`      | GET    | 🕗     |       |
| `/api/PromptHistories/{id}` | GET    | 🕗     |       |
| `/api/PromptHistories`      | POST   | 🕗     |       |
| `/api/PromptHistories/{id}` | PUT    | 🕗     |       |
| `/api/PromptHistories/{id}` | DELETE | 🕗     |       |

---

## 🧠 ExerciseController

| Endpoint              | Method | Status | Notes |
| --------------------- | ------ | ------ | ----- |
| `/api/Exercises`      | GET    | 🕗     |       |
| `/api/Exercises/{id}` | GET    | 🕗     |       |
| `/api/Exercises`      | POST   | 🕗     |       |
| `/api/Exercises/{id}` | PUT    | 🕗     |       |
| `/api/Exercises/{id}` | DELETE | 🕗     |       |
