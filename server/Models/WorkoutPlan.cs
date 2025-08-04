using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;


        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public List<WorkoutDay> WorkoutDays { get; set; } = new();

    }
}