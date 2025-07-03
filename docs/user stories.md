# User Stories for Fitness Tracker Application

## User Registration and Authentication

- As a new user, I want to create an account with my email and password so that I can start using the app.
- As a returning user, I want to log in with my email and password so I can access my saved data.
- As a logged-in user, I want to remain authenticated across sessions using a token.

## Onboarding with LLM

- As a new user, I want to interact with a chatbot where I can describe my workout goals and preferences.
- As a user, I want the chatbot to generate a personalized weekly workout plan based on my input.
- As a user, I want to review and accept the proposed plan before it becomes active.

## Workout Plan Management

- As a user, I want to view my current workout plan in a weekly format.
- As a user, I want to see which exercises I have on each day.
- As a user, I want to change individual exercises if I don't like a suggestion.
- As a user, I want to see the sets, reps, weights, or time required for each exercise.

## Exercise Library Integration

- As a user, I want to view information (description, muscles used, equipment) for each exercise.
- As a user, I want the system to only recommend exercises that match my available equipment and preferences.

## Workout Logging and Tracking

- As a user, I want to log the workouts I complete, including date, sets, reps, time, and weight used.
- As a user, I want to track my performance over time for each exercise.
- As a user, I want to view progress in visual form, such as charts or summaries.

## Prompt History and Feedback

- As a user, I want to view a history of the prompts I've sent to the LLM and the responses I've received.
- As a user, I want to send follow-up prompts to adjust my workout plan.

## Backend & Security

- As a developer, I want to ensure all APIs are secured with JWT so unauthorized users cannot access or manipulate data.
- As a developer, I want all prompt and response history to be logged to help improve service and personalization.

## Hosting and Infrastructure

- As a developer, I want the entire application hosted on Azure using App Services, SQL Database, and OpenAI for LLM responses.
- As a developer, I want to monitor usage and error logs via Azure tools.

---

This list can be used to inform development sprints, testing, and feature tracking.

