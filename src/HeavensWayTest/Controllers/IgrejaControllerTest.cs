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
    public class IgrejaControllerTest
    {
        private IgrejaController _controller;
        private readonly IIgrejaRepository _repository;
        private readonly IEnderecoService _enderecoService;

        public IgrejaControllerTest()
        {
            _repository = Substitute.For<IIgrejaRepository>();
            _enderecoService = Substitute.For<IEnderecoService>();
            _controller = new IgrejaController(_repository, _enderecoService);
        }

        [Fact]
        public void Get_By_Id()
        {
            var igreja = new Igreja{ Id = 1, Nome = "Nome Teste", DistritoId = 1, EnderecoId = 1 };
            var igrejaDto = new IgrejaDto(igreja);

            _repository.GetById(igreja.Id).Returns(igreja);

            var result = _controller.GetById(igreja.Id);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<IgrejaDto>(okResult.Value);

            var igrejaResult = okResult.Value as IgrejaDto;
            Assert.Equal(igrejaDto.Nome, igrejaResult.Nome);
            Assert.Equal(igrejaDto.DistritoId, igrejaResult.DistritoId);
            Assert.Equal(igrejaDto.EnderecoId, igrejaResult.EnderecoId);
        }

        [Fact]
        public void Get_By_Distrito()
        {
            var distritoId = 1;
            var igreja1 = new Igreja{ Id = 1, Nome = "Nome Teste 1", DistritoId = distritoId, EnderecoId = 1 };
            var igreja2 = new Igreja{ Id = 2, Nome = "Nome Teste 2", DistritoId = distritoId, EnderecoId = 2 };
            var igrejas = new List<Igreja>
            {
                igreja1,
                igreja2
            };

            _repository.GetByDistrito(distritoId).Returns(igrejas);

            var result = _controller.GetByDistrito(distritoId);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void Get_All()
        {
            var igreja1 = new Igreja{ Id = 1, Nome = "Nome Teste 1", DistritoId = 1, EnderecoId = 1 };
            var igreja2 = new Igreja{ Id = 2, Nome = "Nome Teste 2", DistritoId = 2, EnderecoId = 2 };
            var igrejas = new List<Igreja>
            {
                igreja1,
                igreja2
            };

            _repository.GetAll().Returns(igrejas);

            var result = _controller.GetAll();
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
