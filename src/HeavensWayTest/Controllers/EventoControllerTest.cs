using HeavensWayApi.Controllers;
using HeavensWayApi.Dto;
using HeavensWayApi.Entities;
using HeavensWayApi.Repositories.Interfaces;
using HeavensWayApi.Services;
using HeavensWayApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace HeavensWayTest.Controllers
{
    public class EventoControllerTest
    {
        private EventoController _controller;
        private readonly IEventoRepository _repository;
        private readonly IIgrejaRepository _igrejaRepository;
        private readonly IEventoService _eventoService;

        public EventoControllerTest()
        {
            _repository = Substitute.For<IEventoRepository>();
            _igrejaRepository = Substitute.For<IIgrejaRepository>();
            _eventoService = Substitute.For<IEventoService>();
            _controller = new EventoController(_repository, _eventoService, _igrejaRepository);
        }

        [Fact]
        public void Get_By_Id()
        {
            var evento = new Evento{ Id = 1, Descricao = "Reuniao", TipoEventoId = 1, DataInicio = DateTime.Now, DataFim = DateTime.Now };
            var eventoDto = new EventoDto(evento);

            _repository.GetById(evento.Id).Returns(evento);

            var result = _controller.GetById(evento.Id);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<EventoDto>(okResult.Value);

            var eventoResult = okResult.Value as EventoDto;
            Assert.Equal(eventoDto.Descricao, eventoResult.Descricao);
            Assert.Equal(eventoDto.DataInicio, eventoResult.DataInicio);
            Assert.Equal(eventoDto.DataFim, eventoResult.DataFim);
            Assert.Equal(eventoDto.TipoEventoId, eventoResult.TipoEventoId);
        }

        [Fact]
        public void Get_All()
        {
            var evento1 = new Evento{ Id = 1, Descricao = "Reuniao", TipoEventoId = 1, DataInicio = DateTime.Now, DataFim = DateTime.Now };
            var evento2 = new Evento{ Id = 2, Descricao = "Reuniao", TipoEventoId = 2, DataInicio = DateTime.Now, DataFim = DateTime.Now };
            var eventos = new List<Evento>
            {
                evento1,
                evento2
            };

            _repository.GetAll().Returns(eventos);

            var result = _controller.GetAll();
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
