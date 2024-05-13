using HeavensWayApi.Repositories;
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [OutputCache]
    public class TipoEventoController : ControllerBase
    {
        private readonly TipoEventoRepository _repository;
        public TipoEventoController(TipoEventoRepository repository)
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
        public IActionResult GetByCep(string description)
        {
            var tiposEvento = _repository.GetByDescription(description);

            var tiposEventoDto = tiposEvento.Select(te => new TipoEventoDto(te));
            return Ok(tiposEventoDto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tiposEvento = _repository.GetAll();
                
            return Ok(tiposEvento);
        }

        [HttpPost]
        public IActionResult Create(TipoEventoDto dto)
        {
            var tipoEvento = new TipoEvento(dto);
            _repository.Create(tipoEvento);
            return Ok();
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