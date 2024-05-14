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
    public class TipoEventoController : ControllerBase
    {
        private readonly ITipoEventoRepository _repository;
        public TipoEventoController(ITipoEventoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tipoEvento = _repository.GetById(id);

            if(tipoEvento == null)
                return NotFound(new { Message = "Not Found"});

            var distritoDto = new TipoEventoDto(tipoEvento);
            return Ok(distritoDto);
        }

        [HttpGet("descricao/{description}")]
        public IActionResult GetByDescription(string description)
        {
            var tiposEvento = _repository.GetByDescription(description);
            if(tiposEvento.ToList().Count == 0)
                return NotFound(new {Message = "Nenhum registro encontrado"});

            var tiposEventoDto = tiposEvento.Select(te => new TipoEventoDto(te));
            return Ok(tiposEventoDto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tiposEvento = _repository.GetAll();
            if(tiposEvento.ToList().Count == 0)
                return Ok(new {Message = "Nenhum registro encontrado"});
                
            return Ok(tiposEvento);
        }

        [HttpPost]
        public IActionResult Create(TipoEventoDto dto)
        {
            var tipoEvento = new TipoEvento(dto);
            _repository.Create(tipoEvento);
            return Created("/tiposevento", tipoEvento);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TipoEventoDto dto)
        {
            var tipoEvento = _repository.GetById(id);

            if(tipoEvento == null)
                return NotFound(new { Message = "Not Found"});

            tipoEvento.MapDto(dto);
            _repository.Update(tipoEvento);
            return Ok(tipoEvento);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tipoEvento = _repository.GetById(id);

            if(tipoEvento == null)
                return NotFound(new { Message = "Not Found"});

            _repository.Delete(tipoEvento);
            return NoContent();
        }
    }
}