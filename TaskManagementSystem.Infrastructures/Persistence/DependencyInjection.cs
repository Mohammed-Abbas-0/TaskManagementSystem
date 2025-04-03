using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interface;
using TaskManagementSystem.Infrastructures.Repositories;
using TaskManagementSystem.Infrastructures.Services;
using TaskManagementSystem.Interface.Repositories;

namespace TaskManagementSystem.Infrastructures.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<AppDbContext>(idx => idx.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Services.AddScoped<ITaskRepository, TaskRepository>();

            Services.AddScoped<IAuthService, AuthService>();

            Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return Services;

        }
    }
}
