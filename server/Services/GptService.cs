using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace server.Services.Ai
{
    public class GptService
    {

        /* This is just a mock up of what it could look like once it is done. 

        We can call it:
        From a PromptHistoryController when a user submits a new prompt.

        From a WorkoutPlanService if we're generating plans dynamically.

        Or from anywhere else business logic needs GPT. */

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GptService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> SendPromptAsync(string prompt)
        {
            var endpoint = _configuration["AzureOpenAI:Endpoint"];
            var key = _configuration["AzureOpenAI:ApiKey"];
            var deployment = _configuration["AzureOpenAI:Deployment"];

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("api-key", key);

            var requestBody = new
            {
                messages = new[]
                {
                    new { role = "system", content = @"You are a helpful personal trainer assistant API. 
                                                        Respond only in strict JSON format, with no extra commentary.
                                                         
                                                        Only recommend exercises, rep schemes, rest intervals, and weekly frequencies based on established evidence from sports science, physical therapy, and strength & conditioning research. 
                                                        Use guidelines from organizations such as the NSCA, ACSM, and peer-reviewed research. 
                                                        Favor compound, functional movements, progressive overload principles, and specificity for goals like hypertrophy, strength, endurance, or VO₂ max.
                                                        Please include 3-5 excersices per day, unless otherwise specified
                                                        The structure must follow this format:

                                                        {
                                                        ""name"": ""Plan name here"",
                                                        ""goal"": ""User's fitness goal here"",
                                                        ""workoutDays"": [
                                                            {
                                                            ""dayOfTheWeek"": ""2025-07-07T00:00:00"",
                                                            ""notes"": ""Focus or guidance for this workout day"",
                                                            ""workoutExercises"": [
                                                                {
                                                                ""sets"": 3,
                                                                ""reps"": 10,
                                                                ""targetWeight"": 60.0,
                                                                ""targetTime"": ""00:01:30"",
                                                                ""exerciseName"": ""Barbell Squat""
                                                                }
                                                            ]
                                                            }
                                                        ]
                                                        }

                                                        'dayOfTheWeek': Use ISO format (e.g. '2025-07-07T00:00:00').
                                                        'targetTime': Use 'HH:mm:ss' format (e.g. '00:01:30'). This is only relevant for timed excersices such as running or planks. reply 00:00:00 for when it's not relevant
                                                        'exerciseName': Use common exercise names like 'Push-Up', 'Deadlift', etc.
                                                        Never include any text outside the JSON object."},
                    new { role = "user", content = prompt }
                },
                max_tokens = 1200,

            };

            var response = await _httpClient.PostAsync(
                $"{endpoint}/openai/deployments/{deployment}/chat/completions?api-version=2024-02-15-preview",
                new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
            );

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString()!;
        }
    }
}

/* 
Pseudo code of how we can parse these replies

var exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Name == dto.ExerciseName);
if (exercise == null)
{
    exercise = new Exercise { Name = dto.ExerciseName,  default group/equipment  };
    _context.Exercises.Add(exercise);
    await _context.SaveChangesAsync();
} 

*/