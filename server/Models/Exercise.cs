using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Equipment { get; set; } = string.Empty;
        public string PrimaryMuscleGroup { get; set; } = string.Empty;
        public string SecondaryMuscleGroup { get; set; } = string.Empty;

    }
}