using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MoneyTracker.Application.RepositoryContracts;
using MoneyTracker.Domain.Entities.Identity;

namespace MoneyTracker.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IAuthRepository _userRepo;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IAuthRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new KeyNotFoundException(nameof(request));
            }
            User? userName = await _userRepo.Get(u => u.UserName == request.registerDto.FullName);
            if(userName != null)
            {
                throw new ArgumentException("user name already exists");
            }
            User? userEmail= await _userRepo.Get(u => u.Email == request.registerDto.Email);
            if(userEmail != null)
            {
                throw new ArgumentException("User Email already exists");
            }
            //User userToDb = _mapper.Map<User>(request.registerDto);
            User userToDb = new User();
            userToDb.Gender = request.registerDto.Gender;
            userToDb.Email = request.registerDto.Email;
            userToDb.UserName = request.registerDto.FullName;
            userToDb.PhoneNumber = request.registerDto.PhoneNumber;
            userToDb.Address = request.registerDto.Address;
            userToDb.Country = request.registerDto.Country;
            userToDb.BirthDate = request.registerDto.BirthDate;
            userToDb.Role = request.registerDto.Role;
            var hashedPassword = new PasswordHasher<User>().HashPassword(userToDb, request.registerDto.Password);
            userToDb.PasswordHash = hashedPassword;
            User userInDb = await _userRepo.Create(userToDb);

            return "Success";

        }
    }
}
