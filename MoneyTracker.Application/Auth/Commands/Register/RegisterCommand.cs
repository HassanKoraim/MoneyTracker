using MediatR;
using MoneyTracker.Application.DTOs.UserDTOs;

namespace MoneyTracker.Application.Auth.Commands.Register
{
    public record RegisterCommand(RegisterUserDto registerDto) : IRequest<string>;
}
