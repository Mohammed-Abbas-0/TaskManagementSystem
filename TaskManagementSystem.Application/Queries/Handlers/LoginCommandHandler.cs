using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interface;
using TaskManagementSystem.Interface.Dtos;
using TaskManagementSystem.Interface.Repositories;

namespace TaskManagementSystem.Application.Queries.Handlers
{
    public class LoginCommand:IRequestHandler<TokenRequestCommand, AuthModel>
    {
        private readonly IAuthService _authService;
        public LoginCommand( IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthModel> Handle(TokenRequestCommand model,CancellationToken cancellationToken)
        {
            if(model is not null)
            {
                var loginDto = new LoginDto { Email=model.Email,Password=model.Password };

                var requestLogin = await _authService.LoginAsync(loginDto);
                if (loginDto is not null)
                {
                    return requestLogin;
                }

            }
            return new AuthModel();
        }

        
    }
}
