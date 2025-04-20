using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Interface.Dtos;

namespace TaskManagementSystem.Interface.Repositories
{
    public interface IGenericInterface<T,TKey>
    {
        Task<List<T>> Find(Expression<Func<T,bool>> filters,Expression<Func<T, TKey>> orderBy);
    }
}
