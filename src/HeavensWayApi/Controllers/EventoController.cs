using HeavensWayApi.Repositories;
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EventoController : ControllerBase
    {
        private readonly EventoRepository _repository;
        public EventoController(EventoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var evento = _repository.GetById(id);

            if(evento == null)
                return NotFound(new { Message = "Not Found"});

            var eventoDto = new EventoDto(evento);
            return Ok(eventoDto);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var eventos = _repository.GetAll();
                
            return Ok(eventos);
        }

        [HttpPost]
        public IActionResult Create(EventoDto dto)
        {
            var evento = new Evento(dto);
            _repository.Create(evento);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, EventoDto dto)
        {
            var evento = _repository.GetById(id);

            if(evento == null)
                return NotFound(new { Message = "Not Found"});

            evento.MapDto(dto);
            _repository.Update(evento);
            return Ok(evento);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var evento = _repository.GetById(id);

            if(evento == null)
                return NotFound(new { Message = "Not Found"});

            _repository.Delete(evento);
            return NoContent();
        }
    }
}