using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Interface.Dtos
{
    public class RegisterModel
    {
        public required string Email { get;set; }
        public required string Password { get;set; }
        public required string UserName { get;set; }
        public required string FirstName { get;set; }
        public required string LastName { get;set; }
    }
}
