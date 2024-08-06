using ApiCatalogoTeste2.DTO_s;
using ApiCatalogoTeste2.Models;
using ApiCatalogoTeste2.Services.TokenServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCatalogoTeste2.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManeger;
    private readonly RoleManager<IdentityRole> _roleMAneger;
    private readonly IConfiguration _configuration;

    public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManeger, RoleManager<IdentityRole> roleMAneger, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManeger = userManeger;
        _roleMAneger = roleMAneger;
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManeger.FindByNameAsync(model.UserName!);

        if (user is not null && await _userManeger.CheckPasswordAsync(user, model.PassWord!))
        {
            var UserRoles = await _userManeger.GetRolesAsync(user);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in UserRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidInMinutes);

            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidInMinutes);
            user.RefreshToken = refreshToken;

            await _userManeger.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Exp = token.ValidTo
            });
        }

        return Unauthorized(new Response() { Status = false, Message = "Usuario ou senha incorretos" });
    }

    [HttpPost("Registro")]
    public async Task<ActionResult> RegistroDeUsuario([FromBody] RegisterModel model)
    {
        var existeUsuario = await _userManeger.FindByNameAsync(model.Username!);

        if (existeUsuario is not null)
            return BadRequest(new Response() { Status = false, Message = "Usuario já cadastrado" });

        ApplicationUser user = new ()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var result = await _userManeger.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
        {
            string? mensagemDeErro = ""; 

            foreach(var erro in result.Errors)
            {
                mensagemDeErro += erro.Description + ", "; 
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response() { Status = false, Message = mensagemDeErro });
        }

        return StatusCode(StatusCodes.Status201Created, new Response() { Status = true, Message = "Usuario criado com sucesso" });

    }

    [HttpPost("RefreshToken")]
    public async Task<ActionResult> RefreshToken([FromBody]TokenModel token)
    {
        if(token is null)
            return BadRequest();

        string? accessToken = token.AccessToken ?? throw new ArgumentNullException(nameof(token));

        string? RefreshToken = token.RefreshToken ?? throw new ArgumentNullException(nameof(token));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);

        if (principal == null)
            return BadRequest();

        string? username = principal.Identity!.Name; 

        var user = await _userManeger.FindByNameAsync(username!);

        if(user == null || user.RefreshToken != RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now){
            return BadRequest("Token Inválido");    
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManeger.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken
        });

    }

    [Authorize]
    [HttpPost("RevokeToken")]
    public async Task<ActionResult> Revoke([FromQuery]string? username)
    {
        var user = await _userManeger.FindByNameAsync(username!);

        if (user is null)
            return NotFound("Usuario não encontrado");

        user.RefreshToken = null;
        await _userManeger.UpdateAsync(user);

        return NoContent();
    }
}

