
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;
using HeavensWayApi.Repositories.Interfaces;
using HeavensWayApi.Services.Interfaces;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OutputCache]
    [Authorize(Roles = "Admin")]
    public class IgrejaController : ControllerBase
    {
        private readonly IIgrejaRepository _repository;
        private readonly IEnderecoService _enderecoService;
        public IgrejaController(IIgrejaRepository repository, IEnderecoService enderecoService)
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
            if(igrejas.ToList().Count == 0)
                return Ok(new {Message = "Nenhum registro encontrado"});
                
            return Ok(igrejas);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var igrejas = _repository.GetAll();
            if(igrejas.ToList().Count == 0)
                return Ok(new {Message = "Nenhum registro encontrado"});
                
            return Ok(igrejas);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(CreateIgrejaDto dto)
        {
            var endereco = _enderecoService.Create(dto.Cep);
            if(endereco == null)
                return BadRequest();

            var igreja = new Igreja(dto, endereco.Id);
            _repository.Create(igreja);
            return Created("/igrejas", igreja);
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