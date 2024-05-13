using HeavensWayApi.Controllers;
using HeavensWayApi.Dto;
using HeavensWayApi.Entities;
using HeavensWayApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace HeavensWayTest.Controllers
{
    public class TipoEventoControllerTest
    {
        private TipoEventoController _controller;
        private readonly ITipoEventoRepository _repository;

        public TipoEventoControllerTest()
        {
            _repository = Substitute.For<ITipoEventoRepository>();
            _controller = new TipoEventoController(_repository);
        }

        [Fact]
        public void Get_By_Id()
        {
            var tipoEvento = new TipoEvento
            { 
                Id = 1,
                Descricao = "Descricao Teste"
            };
            var tipoEventoDto = new TipoEventoDto(tipoEvento);

            _repository.GetById(tipoEvento.Id).Returns(tipoEvento);

            var result = _controller.GetById(tipoEvento.Id);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<TipoEventoDto>(okResult.Value);
            
            var tipoEventoResult = okResult.Value as TipoEventoDto;
            Assert.Equal(tipoEventoDto.Descricao, tipoEventoResult.Descricao);
        }

        [Fact]
        public void Get_By_Description()
        {
            string descricao = "Descricao Teste";
            var tipoEvento1 = new TipoEvento
            { 
                Id = 1,
                Descricao = "Descricao Teste 1"
            };
            var tipoEvento2 = new TipoEvento
            { 
                Id = 1,
                Descricao = "Descricao Teste 2"
            };
            var tiposEvento = new List<TipoEvento>
            {
                tipoEvento1,
                tipoEvento2
            };

            _repository.GetByDescription(descricao).Returns(tiposEvento);

            var result = _controller.GetByDescription(descricao);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void Get_All()
        {
            var tipoEvento1 = new TipoEvento
            { 
                Id = 1,
                Descricao = "Descricao Teste 1"
            };
            var tipoEvento2 = new TipoEvento
            { 
                Id = 1,
                Descricao = "Descricao Teste 2"
            };
            var tiposEvento = new List<TipoEvento>
            {
                tipoEvento1,
                tipoEvento2
            };

            _repository.GetAll().Returns(tiposEvento);

            var result = _controller.GetAll();
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
