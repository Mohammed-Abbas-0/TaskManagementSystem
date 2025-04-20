using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Queries.Classess;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Interface.Dtos;
using TaskManagementSystem.Interface.Repositories;

namespace TaskManagementSystem.Application.Queries.Handlers
{
    public class GetTaskByCodeCommandHandler : IRequestHandler<GetTaskByCodeCommand, List<TasksDto>>
    {
        private readonly IGenericInterface<Tasks,int> _genericInterface;
        public GetTaskByCodeCommandHandler(IGenericInterface<Tasks, int> genericInterface)
        {
            _genericInterface = genericInterface;
        }
        public async Task<List<TasksDto>> Handle(GetTaskByCodeCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<Tasks, int>> orderBy = x => x.Id;

            var tasks = await _genericInterface.Find(
                x => EF.Functions.Like(x.FullName, $"%{request.Code}%") ||
                     EF.Functions.Like(x.Description, $"%{request.Code}%"),
                orderBy
            );

            var tasksDtos = tasks.Select(task => new TasksDto(task.Id, task.FullName, task.Description)).ToList();

            return tasksDtos;
        }
    }
    
}
