using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Queries.Classess
{
    public class GetTaskByIdQuery:IRequest<Tasks>
    {
        public int Id { get; set; }
    }
}
