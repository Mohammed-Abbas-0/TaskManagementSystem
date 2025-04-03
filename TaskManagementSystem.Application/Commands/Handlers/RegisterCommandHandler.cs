using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Commands.Classess;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interface;
using TaskManagementSystem.Interface.Dtos;
using TaskManagementSystem.Interface.Repositories;

namespace TaskManagementSystem.Application.Commands.Handlers
{
    public class RegisterCommandHandler:IRequestHandler<RegisterCommand,AuthModel>
    {
        private readonly IAuthService _authService;
        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthModel> Handle(RegisterCommand register,CancellationToken cancellationToken)
        {
            var authModel = await _authService.RegisterAsync(register);
            return authModel;
        }
    }
}
