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
    public class DistritoController : ControllerBase
    {
        private readonly IDistritoRepository _repository;
        public DistritoController(IDistritoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var distrito = _repository.GetById(id);

            if(distrito == null)
                return NotFound(new { Message = "Not Found"});

            var distritoDto = new DistritoDto(distrito);
            return Ok(distritoDto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var distritos = _repository.GetAll();
            if(distritos.ToList().Count == 0)
                return Ok(new {Message = "Nenhum registro encontrado"});

            return Ok(distritos);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(DistritoDto dto)
        {
            var distrito = new Distrito(dto);
            _repository.Create(distrito);
            return Created("/distritos", distrito);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, DistritoDto dto)
        {
            var distrito = _repository.GetById(id);

            if(distrito == null)
                return NotFound(new { Message = "Not Found"});

            distrito.MapDto(dto);
            _repository.Update(distrito);
            return Ok(distrito);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var distrito = _repository.GetById(id);

            if(distrito == null)
                return NotFound(new { Message = "Not Found"});

            _repository.Delete(distrito);
            return NoContent();
        }
    }
}