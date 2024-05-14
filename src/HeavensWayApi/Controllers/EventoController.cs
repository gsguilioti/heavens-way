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
        [Authorize]
        public IActionResult GetById(int id)
        {
            var evento = _repository.GetById(id);

            if(evento == null)
                return NotFound(new { Message = "Not Found"});

            var eventoDto = new EventoDto(evento);
            return Ok(eventoDto);
        }

        [HttpGet("usuario/{id}")]
        [Authorize]
        public IActionResult GetByUsuario(int id)
        {
            var eventos = _repository.GetByUsuario(id);
            if(eventos.ToList().Count == 0)
                return Ok(new {Message = "Sem registros cadastrados"});
                
            return Ok(eventos);
        }

        [HttpGet("igreja/{id}")]
        [Authorize]
        public IActionResult GetByIgreja(int id)
        {
            var eventos = _repository.GetByIgreja(id);
            if(eventos.ToList().Count == 0)
                return Ok(new {Message = "Nenhum registro encontrado"});
                
            return Ok(eventos);
        }

        [HttpGet("inscritos/{id}")]
        [Authorize]
        public IActionResult GetInscritos(int id)
        {
            var eventos = _repository.GetInscritos(id);
            if(eventos.ToList().Count == 0)
                return Ok(new {Message = "Nenhum registro encontrado"});
                
            return Ok(eventos);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var eventos = _repository.GetAll();
            if(eventos.ToList().Count == 0)
                return Ok(new {Message = "Nenhum registro encontrado"});
                
            return Ok(eventos);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(EventoDto dto)
        {
            var evento = new Evento(dto);
            _repository.Create(evento);
            return Created("/eventos", evento);
        }

        [HttpPost("inscrever/{eventoId}/{usuarioId}")]
        [Authorize]
        public IActionResult Inscrever(int eventoId, int usuarioId)
        {
            if(_eventoService.Inscrever(eventoId, usuarioId))
                return Ok();

            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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