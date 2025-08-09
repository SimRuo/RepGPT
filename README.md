# RepGPT

https://wonderful-flower-0a7716003.1.azurestaticapps.net/

This is a personal project where I explored how AI could help with creating and tracking workout plans. This was developed in conjunction with AZ-900 studies.
It combines an **ASP.NET Core backend**, **React frontend**, and **Azure OpenAI** to generate personalized workout schedules and track progress.

---

## Demo

![Demo](docs/Demo%20gif.gif)

---

## Why I Built This

I wanted to learn how to:
- Integrate an AI model (Azure OpenAI) into a real app.
- Build a full-stack project using **React (Vite + MUI)** and **ASP.NET Core**. I already had some experience with React + NodeJS but i wanted to get into Azure and .NET backends.
- Deploy everything to Azure (Static Web Apps, App Service, SQL Database).

This project gave me an excuse to work through cloud deployment, dabble in authentication, and using an AI API for structured data.

---

## How It Works

1. The frontend (React) calls the backend API.
2. The backend sends a prompt to Azure OpenAI, asking for a structured JSON workout plan.
3. The backend saves and returns that plan.
4. The frontend displays it and allows users to log workouts.

---

## Architecture

![System Architecture](docs/System%20Architecture%20Diagram.png)

---

## Data Model

![Entity Relationship Diagram](docs/RepGPT%20Entity%20Relationship%20Diagram.png)

---

## Running It Locally

### Requirements
- Node.js 20+
- .NET 8 SDK
- Access to an Azure SQL DB or local SQL server

### Frontend
```bash
cd client
npm install
npm run dev
```

### Backend
```bash
cd server
dotnet restore
dotnet run
```

### Secrets
Handled through Azure environment variables (backend) and github secrets (frontend)

### Deployment
- Frontend: Azure Static Web Apps
- Backend: Azure App Service
- Database: Azure SQL Database

### What I Learned
- Using Azure OpenAI to produce structured JSON outputs
- Deploying a full-stack app to Azure
- Working with environment variables and secrets securely
- Combining a React SPA with an ASP.NET API
- DTO pattern
- Basic CI/CD
- There's almost too much to list here.

### Next Steps / Ideas
- ~~Proper React routing so manually setting the URL doesnt break everything~~
- Clean up unused code/files left over from development
- Complete a proper CI/CD pipeline, current one is very basic
- UI improvements
- AI personalization for truly dynamic progressive overload
- Database refactoring for more advanced periodizations and support for cardio plans
