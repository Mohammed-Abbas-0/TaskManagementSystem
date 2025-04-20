using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Infrastructures.Persistence;
using TaskManagementSystem.Interface.Repositories;

namespace TaskManagementSystem.Infrastructures.Repositories
{
    public class GenericInterface<T, TKey> : IGenericInterface<T, TKey> where T : class
    {
        private readonly AppDbContext _context;
        public GenericInterface(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<List<T>> Find(Expression<Func<T, bool>> filters, Expression<Func<T, TKey>> orderBy)
        {
            var result = await _context.Set<T>()
                                 .AsNoTracking()
                                 .Where(filters)
                                 .OrderByDescending(orderBy)
                                 .ToListAsync();

            return result ?? new List<T>();
        }

        
    }
}
