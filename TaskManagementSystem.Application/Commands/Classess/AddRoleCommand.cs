using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Interface.Dtos;

namespace TaskManagementSystem.Application.Commands.Classess
{
    public class AddRoleCommand:IRequest<AuthModel>
    {
        public required string UserId { get; set; }
        public required string Role { get; set; }
    }
}
