using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Interface.Dtos;

namespace TaskManagementSystem.Application.Queries.Handlers
{
    public class TokenRequestCommand : IRequest<AuthModel>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
