using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;
using HeavensWayApi.Repositories.Interfaces;

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
        private readonly IUsuarioRepository _repository;

        public UsuarioController(UserManager<Usuario> userManager,
                                 SignInManager<Usuario> signInManager,
                                 RoleManager<IdentityRole<int>> roleManager,
                                 IConfiguration configuration,
                                 IUsuarioRepository repository)

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

            if (usuario == null)
                return NotFound(new { Message = "Not Found" });

            return Ok(usuario);
        }

        [HttpGet("igreja/{id}")]
        public IActionResult GetByIgreja(int id)
        {
            var usuarios = _repository.GetByIgreja(id);

            var usuariosoDto = usuarios.Select(u => new GetUsuarioDto(u));
            if (usuariosoDto.ToList().Count == 0)
                return Ok(new { Message = "Nenhum registro encontrado" });

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
            if (users.Count == 0)
                return Ok(new { Message = "Nenhum registro encontrado" });

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioDto dto)
        {
            var usuario = new Usuario(dto);
            var result = await _userManager.CreateAsync(usuario, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var checkRole = await _roleManager.FindByNameAsync(dto.Role);
            if (checkRole == null)
                await _roleManager.CreateAsync(new IdentityRole<int>() { Name = dto.Role });

            await _userManager.AddToRoleAsync(usuario, dto.Role);

            return Created("/usuarios", usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UsuarioDto dto)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());

            if (usuario == null)
                return NotFound(new { Message = "Not Found" });

            usuario.MapDto(dto);

            var result = await _userManager.UpdateAsync(usuario);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());

            if (usuario == null)
                return NotFound(new { Message = "Not Found" });

            var result = await _userManager.DeleteAsync(usuario);
            if (!result.Succeeded)
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
            if (!result.Succeeded)
                return Unauthorized(new { message = "Invalid username or password" });

            var token = "Bearer " + GenerateJwtToken(usuario);
            return Ok(new { token });
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var role = GetRole(usuario);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role.Result),
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

        private async Task<string> GetRole(Usuario usuario)
        {
            var roles = await _userManager.GetRolesAsync(usuario);
            if (roles.Count > 0)
                return roles[0];

            return "User";
        }
    }
}