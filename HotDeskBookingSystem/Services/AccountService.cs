using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotDeskBookingSystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HotDeskBookingSystem.Services
{
    public interface IAccountService
    {
        bool RegisterEmployee(RegisterEmployeeDto dto);
        string GenerateJwt(LoginEmployeeDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly DeskDbContext _dbContext;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(DeskDbContext dbContext, IPasswordHasher<Employee> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public bool RegisterEmployee(RegisterEmployeeDto dto)
        {
            var emailTaken = _dbContext
                .Employees
                .FirstOrDefault(e => e.Email == dto.Email);

            if (emailTaken != null) return false;

            var newEmployee = new Employee()
            {
                FirstName = dto.FirstName,
                SecondName = dto.SecondName,
                Email = dto.Email,
                RoleId = dto.RoleId
            };

            var hashedPassword = _passwordHasher.HashPassword(newEmployee, dto.Password);
            newEmployee.PasswordHash = hashedPassword;

            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();

            return true;
        }

        public string GenerateJwt(LoginEmployeeDto dto)
        {
            var employee = _dbContext
                .Employees
                .Include(e => e.Role)
                .FirstOrDefault(e => e.Email == dto.Email);

            if(employee is null)
            {
                return "nouser";
            };

            var result = _passwordHasher.VerifyHashedPassword(employee, employee.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                return "invalidpass";
            };

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{employee.FirstName} {employee.SecondName}"),
                new Claim(ClaimTypes.Role, $"{employee.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}

