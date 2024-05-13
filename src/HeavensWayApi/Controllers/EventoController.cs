using HeavensWayApi.Repositories;
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using HeavensWayApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;
using HeavensWayApi.Repositories.Interfaces;
using HeavensWayApi.Services.Interfaces;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [OutputCache]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _repository;
        private readonly IEventoService _eventoService;
        public EventoController(IEventoRepository repository, IEventoService eventoService)
        {
            _repository = repository;
            _eventoService = eventoService;
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

        [HttpGet("usuario/{id}")]
        public IActionResult GetByUsuario(int id)
        {
            var eventos = _repository.GetByUsuario(id);
                
            return Ok(eventos);
        }

        [HttpGet("igreja/{id}")]
        public IActionResult GetByIgreja(int id)
        {
            var eventos = _repository.GetByIgreja(id);
                
            return Ok(eventos);
        }

        [HttpGet("inscritos/{id}")]
        public IActionResult GetInscritos(int id)
        {
            var eventos = _repository.GetInscritos(id);
                
            return Ok(eventos);
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

        [HttpPost("inscrever/{eventoId}/{usuarioId}")]
        public IActionResult Inscrever(int eventoId, int usuarioId)
        {
            if(_eventoService.Inscrever(eventoId, usuarioId))
                return Ok();

            return BadRequest();
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