using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interface;
using TaskManagementSystem.Infrastructures.Persistence;

namespace TaskManagementSystem.Infrastructures.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        // readonly => تأكيد الثبات (Immutability):

        /*باستخدام readonly،
         * نضمن أن المتغير
         * _context لن يتغير بعد تعيينه في المُنشئ.
         * هذا يجعل الكود أكثر أمانًا،
         * لأنه لا يمكن تغيير قيمة المتغير بعد تهيئته.


        */

        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddTaskAsync(Tasks task)
        {
            await _context.Tasks.AddAsync(task);
            return await Save();
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            Tasks? task = await _context.Tasks.FindAsync(id);
            if(task is null)
                return false;
            _context.Tasks.Remove(task);
            return await Save();
        }

        public async Task<IEnumerable<Tasks>> GetAllTasksAsync(int pageNumber,int pageSize) =>
            await _context.Tasks
                                .AsNoTracking()
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();


        public async Task<Tasks?> GetTaskByIdAsync(int id)=>
            await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        




        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0  ? true : false;
        }

        public async Task<bool> UpdateTaskAsync(Tasks task)
        {
            _context.Tasks.Update(task);
            return await Save();
        }
    }
}
