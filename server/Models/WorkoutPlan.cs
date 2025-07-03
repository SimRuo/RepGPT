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


        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int WorkoutDayId { get; set; } // not sure if this should be here, we can now navigate in both directions
        public WorkoutDay WorkoutDay { get; set; } = null!; // not sure if this should be here, we can now navigate in both directions

    }
}