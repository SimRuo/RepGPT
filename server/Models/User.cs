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

        public int WorkoutPlanId { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; } = null!;
        public int WorkoutLogId { get; set; }
        public WorkoutLog WorkoutLog { get; set; } = null!;
        public int PromptHistoryId { get; set; }
        public PromptHistory PromptHistory { get; set; } = null!;

    }
}