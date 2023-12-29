// AuthenticationController.cs

using EmployeeProgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private Users AuthenticateUser(Users user)
    {
        Users _user = null;
        if (user.Username == "admin" && user.Password == "12345")
        {
            _user = new Users { Username = "Suhail Shaik" };
        }
        return _user;
    }

    private string GenerateToken(Users user)
    {
        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    // Add additional claims as needed
                },
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine($"Generated Token: {tokenString}");

            return tokenString;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating token: {ex.Message}");
            throw;
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Login(Users user)
    {
        IActionResult response = Unauthorized();
        var authenticatedUser = AuthenticateUser(user);

        if (authenticatedUser != null)
        {
            var token = GenerateToken(authenticatedUser);
            response = Ok(new { Token = token });
        }

        return response;
    }
}

