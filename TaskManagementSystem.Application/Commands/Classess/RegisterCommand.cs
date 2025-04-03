using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Interface.Dtos;

namespace TaskManagementSystem.Application.Commands.Classess
{
    public class RegisterCommand:RegisterModel,IRequest<AuthModel>
    {
        
    }
}
