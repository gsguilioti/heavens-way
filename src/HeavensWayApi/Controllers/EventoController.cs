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
        private readonly IIgrejaRepository _igrejaRepository;
        private readonly IEventoService _eventoService;
        public EventoController(IEventoRepository repository, IEventoService eventoService, IIgrejaRepository igrejaRepository)
        {
            _repository = repository;
            _eventoService = eventoService;
            _igrejaRepository = igrejaRepository;
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
            var usuarios = _repository.GetInscritos(id);
            if(usuarios.ToList().Count == 0)
                return Ok(new {Message = "Nenhum registro encontrado"});
                
            var usuariosList = new List<UsuarioDto>();
            foreach(var usuario in usuarios)
            {
                usuariosList.Add(new UsuarioDto(usuario));
            }

            return Ok(usuariosList);
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
            var igreja = _igrejaRepository.GetById(dto.IgrejaId);
            if(igreja == null)
                return BadRequest(new { Message = "Igreja não encontrada"} );

            var evento = new Evento(dto);
            _repository.Create(evento);
            _repository.AddIgreja(igreja, evento.Id);
            return Created("/eventos", evento);
        }

        [HttpPost("inscrever/{eventoId}/{usuarioId}")]
        [Authorize]
        public IActionResult Inscrever(int eventoId, int usuarioId)
        {
            if(_eventoService.Inscrever(eventoId, usuarioId))
                return Ok(new {Message = "Usuário inscrito com sucesso!"});

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