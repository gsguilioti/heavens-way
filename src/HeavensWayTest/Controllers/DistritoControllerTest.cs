using HeavensWayApi.Controllers;
using HeavensWayApi.Dto;
using HeavensWayApi.Entities;
using HeavensWayApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace HeavensWayTest.Controllers
{
    public class DistritoControllerTest
    {
        private DistritoController _controller;
        private readonly IDistritoRepository _repository;

        public DistritoControllerTest()
        {
            _repository = Substitute.For<IDistritoRepository>();
            _controller = new DistritoController(_repository);
        }

        [Fact]
        public void Get_By_Id()
        {
            var distrito = new Distrito{ Id = 5, Nome = "alpha" };
            var distritoDto = new DistritoDto(distrito);

            _repository.GetById(distrito.Id).Returns(distrito);

            var result = _controller.GetById(distrito.Id);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            Assert.IsType<DistritoDto>(okResult.Value);
            Assert.Equal(distritoDto.Nome, (okResult.Value as DistritoDto).Nome);
        }

        [Fact]
        public void Get_All()
        {
            var distrito1 = new Distrito{ Id = 5, Nome = "alpha" };
            var distrito2 = new Distrito{ Id = 5, Nome = "beta" };
            var distritos = new List<Distrito>
            {
                distrito1,
                distrito2
            };

            _repository.GetAll().Returns(distritos);

            var result = _controller.GetAll();
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
