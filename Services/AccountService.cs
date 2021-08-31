using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using cookBook.Entities;
using cookBook.Entities.Users;
using cookBook.Exceptions;
using cookBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace cookBook.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);

        string GenerateJwtToken(LoginDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly CookBookDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _hasher;
        private readonly AuthenticationSettings _authenticationSettings;


        public AccountService(CookBookDbContext dbContext, IMapper mapper, IPasswordHasher<User> hasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hasher = hasher;
            _authenticationSettings = authenticationSettings;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            var hashedPassword = _hasher.HashPassword(user, dto.Password);

            user.PasswordHash = hashedPassword;


            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

        }

        public string GenerateJwtToken(LoginDto dto)
        {
            var user = _dbContext
                .Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if (user is null)
            {
                throw new BadRequestException("Wrong email or password");
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Wrong email or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("Nationality", user.Nationality)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);


            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
                claims, expires: expires, signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }
    }
}