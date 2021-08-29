using AutoMapper;
using cookBook.Entities;
using cookBook.Entities.Users;
using cookBook.Models;
using Microsoft.AspNetCore.Identity;

namespace cookBook.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);

    }

    public class AccountService : IAccountService
    {
        private readonly CookBookDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _hasher;


        public AccountService(CookBookDbContext dbContext, IMapper mapper, IPasswordHasher<User> hasher)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hasher = hasher;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            var hashedPassword = _hasher.HashPassword(user, dto.Password);

            user.PasswordHash = hashedPassword;


            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

        }
    }
}