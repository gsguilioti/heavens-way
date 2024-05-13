using HeavensWayApi.Repositories;
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using HeavensWayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class IgrejaController : ControllerBase
    {
        private readonly IgrejaRepository _repository;
        private readonly EnderecoService _enderecoService;
        public IgrejaController(IgrejaRepository repository, EnderecoService enderecoService)
        {
            _repository = repository;
            _enderecoService = enderecoService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var igreja = _repository.GetById(id);

            if(igreja == null)
                return NotFound(new { Message = "Not Found"});

            var igrejaDto = new IgrejaDto(igreja);
            return Ok(igrejaDto);
        }

        [HttpGet("distrito/{id}")]
        public IActionResult GetByDistrito(int id)
        {
            var igrejas = _repository.GetByDistrito(id);
                
            return Ok(igrejas);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var igrejas = _repository.GetAll();
                
            return Ok(igrejas);
        }

        [HttpPost]
        public IActionResult Create(CreateIgrejaDto dto)
        {
            var endereco = _enderecoService.Create(dto.Cep);
            if(endereco == null)
                return BadRequest();

            var igreja = new Igreja(dto, endereco.Id);
            _repository.Create(igreja);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, IgrejaDto dto)
        {
            var igreja = _repository.GetById(id);

            if(igreja == null)
                return NotFound(new { Message = "Not Found"});

            igreja.MapDto(dto);
            _repository.Update(igreja);
            return Ok(igreja);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var igreja = _repository.GetById(id);

            if(igreja == null)
                return NotFound(new { Message = "Not Found"});

            _repository.Delete(igreja);
            return NoContent();
        }
    }
}