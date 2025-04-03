using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Interface.Dtos;

namespace TaskManagementSystem.Interface.Repositories
{
    public interface IAuthService
    {
        Task<AuthModel> LoginAsync(LoginDto model);
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<string> AddRoleAsync(AddRoleModel roleModel);
    }
}
