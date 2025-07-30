using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using server.Controllers;
using server.Data;
using server.Models;
using server.Services;
using server.Services.Ai;

var builder = WebApplication.CreateBuilder(args);

// Load secrets from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RepGPTContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PromptHistoryService>();
builder.Services.AddScoped<ExerciseService>();
builder.Services.AddScoped<WorkoutDayService>();
builder.Services.AddScoped<WorkoutExerciseService>();
builder.Services.AddScoped<WorkoutLogService>();
builder.Services.AddScoped<WorkoutPlanService>();
builder.Services.AddScoped<WorkoutStatisticsService>();
builder.Services.AddHttpClient<GptService>();

builder.Services.AddControllers();
builder.Services.AddHttpClient<GptService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.MapControllers();

app.Run();
