using HeavensWayApi.Repositories;
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    [OutputCache]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;  
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly UsuarioRepository _repository;

        public UsuarioController(UserManager<Usuario> userManager, 
                                 SignInManager<Usuario> signInManager, 
                                 RoleManager<IdentityRole<int>> roleManager,
                                 IConfiguration configuration,
                                 UsuarioRepository repository)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());

            if(usuario == null)
                return NotFound(new { Message = "Not Found"});

            return Ok(usuario);
        }

        [HttpGet("igreja/{id}")]
        public IActionResult GetByIgreja(int id)
        {
            var usuarios = _repository.GetByIgreja(id);

            var usuariosoDto = usuarios.Select(u => new GetUsuarioDto(u));
            return Ok(usuariosoDto);
        }

        [HttpGet("evento/{id}")]
        public IActionResult GetByEvento(int id)
        {
            var usuarios = _repository.GetByEvento(id);

            var usuariosoDto = usuarios.Select(u => new GetUsuarioDto(u));
            return Ok(usuariosoDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.Select(x => new GetUsuarioDto(x)).ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioDto dto)
        {
            var usuario = new Usuario(dto);
            var result = await _userManager.CreateAsync(usuario, dto.Password);

            if(!result.Succeeded)
                return BadRequest(result.Errors);

            var checkAdmin = await _roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>() { Name = "Admin" });
                await _userManager.AddToRoleAsync(usuario, "Admin");
            }
            else
            {
                var checkUser = await _roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await _roleManager.CreateAsync(new IdentityRole<int>() { Name = "User" });

                await _userManager.AddToRoleAsync(usuario, "User");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UsuarioDto dto)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());

            if(usuario == null)
                return NotFound(new { Message = "Not Found"});

            usuario.MapDto(dto);

            var result = await _userManager.UpdateAsync(usuario);
            if(!result.Succeeded)
                return BadRequest(result.Errors);
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());

            if(usuario == null)
                return NotFound(new { Message = "Not Found"});

            var result = await _userManager.DeleteAsync(usuario);
            if(!result.Succeeded)
                return BadRequest(result.Errors);
            
            return Ok();
        }

         [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            var usuario = await _userManager.FindByNameAsync(dto.UserName);
            if (usuario == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(usuario, dto.Password, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var token = "Bearer " + GenerateJwtToken(usuario);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
        }

         private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UserName),
                new Claim(ClaimTypes.Name, usuario.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}