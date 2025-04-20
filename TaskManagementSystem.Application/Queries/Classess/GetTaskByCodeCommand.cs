using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Interface.Dtos;

namespace TaskManagementSystem.Application.Queries.Classess
{
    public class GetTaskByCodeCommand:IRequest<List<TasksDto>>
    {
        public required string Code { get; set; }
    }
}
