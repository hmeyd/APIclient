using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ClientApi.Data;
using ClientApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BCrypt.Net;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly AuthService _authService;
	private readonly AppDbContext _context;

	public UserController(AuthService authService, AppDbContext Context)
	{
		_authService = authService;
		_context = Context;
	}

    [HttpPost("registre")]
    public IActionResult Register([FromBody] UserDto dto)
    {
        var user = new User
        {
            Email = dto.Email,
            PasswordHash = _authService.HashPassword(dto.Password),
            Role = dto.Role
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(new { message = "Utilisateur créé !" });
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] UserDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

        if (user == null)
            return Unauthorized(new { message = "Email introuvable" });

        if (!_authService.VerifyPassword(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "Mot de passe incorrect" });

        // ICI tu dois passer l'objet User, PAS son email
        var token = _authService.GenerateJwtToken(user);

        return Ok(new
        {
            message = "Connexion réussie",
            token
        });
    }
}
