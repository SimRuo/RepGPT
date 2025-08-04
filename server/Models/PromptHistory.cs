using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class PromptHistory
    {
        public int Id { get; set; }
        public string PromptText { get; set; } = string.Empty;
        public string ResponseText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; } // FK back to User
        public User User { get; set; } = null!;
    }
}