using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Interface;

namespace TaskManagementSystem.Application.OrganizingEndpoints
{
    public static class TaskMinimalApi
    {
        public static void MapTaskEndpoints(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("/tasks/getall", async (ITaskRepository taskRepository, int pageNumber = 1, int pageSize = 10) => {

                var tasks = await taskRepository.GetAllTasksAsync(pageNumber, pageSize);
                return Results.Ok(tasks);
            })
            .RequireAuthorization("Admin");
        }
    }
}
