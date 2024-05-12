using HeavensWayApi.Repositories;
using HeavensWayApi.Entities;
using HeavensWayApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HeavensWayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IgrejaController : ControllerBase
    {
        private readonly IgrejaRepository _repository;
        public IgrejaController(IgrejaRepository repository)
        {
            _repository = repository;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var igrejas = _repository.GetAll();
                
            return Ok(igrejas);
        }

        [HttpPost]
        public IActionResult Create(IgrejaDto dto)
        {
            var igreja = new Igreja(dto);
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