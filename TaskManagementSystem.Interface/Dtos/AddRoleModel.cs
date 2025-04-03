using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Interface.Dtos
{
    public class AddRoleModel
    {
        public required string UserId { get; set; }
        public required string Role { get; set; }
    }
}
