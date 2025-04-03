using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Commands.Classess
{
    public class CreateTaskCommand:IRequest<int>
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string UserId { get; set; }
    }
}
