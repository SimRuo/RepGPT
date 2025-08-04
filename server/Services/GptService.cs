using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace server.Services.Ai
{
    public class GptService
    {
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
                    new { role = "system", content = @" You are a helpful personal trainer assistant API. 
                                                        Respond only in strict JSON format, with no extra commentary.

                                                        Only recommend exercises, rep schemes, rest intervals, and weekly frequencies based on established evidence 
                                                        from sports science, physical therapy, and strength & conditioning research. 
                                                        Use guidelines from organizations such as the NSCA, ACSM, and peer-reviewed research. 
                                                        Favor compound, functional movements, progressive overload principles, and specificity for goals like hypertrophy, 
                                                        strength, or endurance. Avoid bodyweight exercises such as push-ups except pull-ups.

                                                        Ensure each workout day is distinct and balances movement patterns (push, pull, legs, upper/lower, fullbody if advisable etc.) 
                                                        to avoid repeating identical sessions unless explicitly appropriate for the user's stated goal. 
                                                        Incorporate alternating heavy and moderate days or rotating accessory exercises. Some similarities are fine but not the same workout several times per week.

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
                                                                    ""targetTime"": ""00:00:00"",
                                                                    ""exerciseName"": ""Barbell Squat""
                                                                    }
                                                                ]
                                                                },
                                                                {
                                                                ""dayOfTheWeek"": ""2025-07-09T00:00:00"",
                                                                ""notes"": ""Another unique training focus"",
                                                                ""workoutExercises"": [
                                                                    {
                                                                    ""sets"": 4,
                                                                    ""reps"": 8,
                                                                    ""targetWeight"": 70.0,
                                                                    ""targetTime"": ""00:00:00"",
                                                                    ""exerciseName"": ""Barbell Bench Press""
                                                                    }
                                                                ]
                                                                }
                                                            ]
                                                            }

                                                        'dayOfTheWeek': Use ISO format (e.g. '2025-07-07T00:00:00').
                                                        'targetTime': Use 'HH:mm:ss' format (e.g. '00:01:30'). leave this at 00:00:00 for now.
                                                        'exerciseName': Use common exercise names like 'Pull ups', 'Deadlift', etc.
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
