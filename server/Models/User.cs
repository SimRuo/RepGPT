using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public WorkoutPlan WorkoutPlan { get; set; } = null!; // one to one relationship
        public List<WorkoutLog> WorkoutLogs { get; set; } = new(); // one to many relationship
        public List<PromptHistory> PromptHistory { get; set; } = new(); // one to many relationship

    }
}