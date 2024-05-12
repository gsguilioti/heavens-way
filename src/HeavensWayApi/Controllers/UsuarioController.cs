using HeavensWayApi.Repositories;
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _repository;
        public UsuarioController(UsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _repository.GetById(id);

            if(usuario == null)
                return NotFound(new { Message = "Not Found"});

            var usuarioDto = new UsuarioDto(usuario);
            return Ok(usuarioDto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var usuarios = _repository.GetAll();
                
            return Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Create(UsuarioDto dto)
        {
            var usuario = new Usuario(dto);
            _repository.Create(usuario);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UsuarioDto dto)
        {
            var usuario = _repository.GetById(id);

            if(usuario == null)
                return NotFound(new { Message = "Not Found"});

            usuario.MapDto(dto);
            _repository.Update(usuario);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _repository.GetById(id);

            if(usuario == null)
                return NotFound(new { Message = "Not Found"});

            _repository.Delete(usuario);
            return NoContent();
        }
    }
}