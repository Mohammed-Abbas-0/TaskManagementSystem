using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Commands.Classess;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Commands.Handlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand,int>
    {
        //private readonly AppDbContext _context;

        public CreateTaskCommandHandler(/*AppDbContext context*/)
        {
           // _context = context;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new Tasks
            {
                FullName = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                
            };

           // _context.Tasks.Add(task);
          //  await _context.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }
}
