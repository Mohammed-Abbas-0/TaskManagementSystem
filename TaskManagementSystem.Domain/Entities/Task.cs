using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Entities
{
    public class Tasks
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required string UserId { get; set; } // Foreign key to User
        public  User? User { get; set; }
    }
}
