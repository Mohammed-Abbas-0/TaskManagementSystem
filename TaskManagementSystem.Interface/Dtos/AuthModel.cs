using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Interface.Dtos
{
    public class AuthModel
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public List<string>? Roles { get; set; }
        public bool? IsAuthenticated { get; set; } = false;
        public string? Message { get; set; }
        public string? RefreshToken { get; set; }

    }
}
