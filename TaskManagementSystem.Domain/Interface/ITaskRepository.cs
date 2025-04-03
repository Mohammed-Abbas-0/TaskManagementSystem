using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tasks>> GetAllTasksAsync(int pageNumber, int pageSize);
        Task<Tasks?> GetTaskByIdAsync(int id);
        Task<bool> AddTaskAsync(Tasks task);
        Task<bool> UpdateTaskAsync(Tasks task);
        Task<bool> DeleteTaskAsync(int id);
        Task<bool> Save();
    }
}
