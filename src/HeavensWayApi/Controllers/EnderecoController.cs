using HeavensWayApi.Repositories;
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoRepository _repository;
        public EnderecoController(EnderecoRepository repository)
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var enderecos = _repository.GetAll();
                
            return Ok(enderecos);
        }

        [HttpPost]
        public IActionResult Create(EnderecoDto dto)
        {
            var endereco = new Endereco(dto);
            _repository.Create(endereco);
            return Ok();
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