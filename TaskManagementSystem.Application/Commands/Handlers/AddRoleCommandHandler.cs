using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Commands.Classess;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Interface.Dtos;

namespace TaskManagementSystem.Application.Commands.Handlers
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, AuthModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AddRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<AuthModel> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            var role = await _roleManager.FindByNameAsync(request.Role);

            if (user is null || role is null)
                return new AuthModel { Message="UserName Or Role Invalid.",IsAuthenticated=false};

            if(await _userManager.IsInRoleAsync(user,role.Name??""))
                return new AuthModel { Message="UserName already have this role.",IsAuthenticated=false};
                

            var result = await _userManager.AddToRoleAsync(user, role.Name??"");
            if (!result.Succeeded)
                return new AuthModel { Message="UserName Or Role Invalid.",IsAuthenticated=false};

            var getUserRoles = await _userManager.GetRolesAsync(user);
            return new AuthModel { Message="Inserted Role to user Successfully",IsAuthenticated=true,UserName = user.UserName,Roles= getUserRoles.ToList() };

        }
    }
}
