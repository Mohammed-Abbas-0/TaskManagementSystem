using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Queries.Classess;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interface;

namespace TaskManagementSystem.Application.Queries.Handlers
{
    public class GetTaskByIdQueryHandler:IRequestHandler<GetTaskByIdQuery, Tasks>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Tasks> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskByIdAsync(request.Id);
            if (task is null)
                return null;
            return task;
        }
    }
}
