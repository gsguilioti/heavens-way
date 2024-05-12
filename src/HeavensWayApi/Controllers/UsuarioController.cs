using HeavensWayApi.Repositories;
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;  
        public UsuarioController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());

            if(usuario == null)
                return NotFound(new { Message = "Not Found"});

            return Ok(usuario);
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
    }
}