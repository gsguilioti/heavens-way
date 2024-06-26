using HeavensWayApi.Repositories;
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;
using HeavensWayApi.Repositories.Interfaces;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache]
    [Authorize(Roles = "Admin")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoRepository _repository;
        public EnderecoController(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var endereco = _repository.GetById(id);

            if(endereco == null)
                return NotFound(new { Message = "Not Found"});

            var enderecoDto = new EnderecoDto(endereco);
            return Ok(enderecoDto);
        }

        [HttpGet("cep/{cep}")]
        public IActionResult GetByCep(string cep)
        {
            var endereco = _repository.GetByCep(cep);

            if(endereco == null)
                return NotFound(new { Message = "Not Found"});

            var enderecoDto = new EnderecoDto(endereco);
            return Ok(enderecoDto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var enderecos = _repository.GetAll();
            if(enderecos.ToList().Count == 0)
                return Ok(new {Message = "Nenhum registro encontrado"});
                
            return Ok(enderecos);
        }

        [HttpPost]
        public IActionResult Create(EnderecoDto dto)
        {
            var endereco = new Endereco(dto);
            _repository.Create(endereco);
            return Created("/enderecos", endereco);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, EnderecoDto dto)
        {
            var endereco = _repository.GetById(id);

            if(endereco == null)
                return NotFound(new { Message = "Not Found"});

            endereco.MapDto(dto);
            _repository.Update(endereco);
            return Ok(endereco);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var endereco = _repository.GetById(id);

            if(endereco == null)
                return NotFound(new { Message = "Not Found"});

            _repository.Delete(endereco);
            return NoContent();
        }
    }
}