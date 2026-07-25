using MediatR;
using MoneyTracker.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Auth.Commands.Login
{
    public record LoginCommand(LoginUserDto loginDto) : IRequest<string>
    {
    }
}
