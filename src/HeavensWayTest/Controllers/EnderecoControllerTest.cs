using HeavensWayApi.Controllers;
using HeavensWayApi.Dto;
using HeavensWayApi.Entities;
using HeavensWayApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace HeavensWayTest.Controllers
{
    public class EnderecoControllerTest
    {
        private EnderecoController _controller;
        private readonly IEnderecoRepository _repository;

        public EnderecoControllerTest()
        {
            _repository = Substitute.For<IEnderecoRepository>();
            _controller = new EnderecoController(_repository);
        }

        [Fact]
        public void Get_By_Id()
        {
            var endereco = new Endereco
            { 
                Id = 1,
                Cep = "87023670",
                Logradouro = "Rua Jacaranda",
                Bairro = "Parque Palmeiras",
                Cidade = "Maringa",
                Estado = "PR",
            };
            var enderecoDto = new EnderecoDto(endereco);

            _repository.GetById(endereco.Id).Returns(endereco);

            var result = _controller.GetById(endereco.Id);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<EnderecoDto>(okResult.Value);
            
            var enderecoResult = okResult.Value as EnderecoDto;
            Assert.Equal(enderecoDto.Cep, enderecoResult.Cep);
            Assert.Equal(enderecoDto.Logradouro, enderecoResult.Logradouro);
            Assert.Equal(enderecoDto.Bairro, enderecoResult.Bairro);
            Assert.Equal(enderecoDto.Cidade, enderecoResult.Cidade);
            Assert.Equal(enderecoDto.Estado, enderecoResult.Estado);
        }

        [Fact]
        public void Get_By_Cep()
        {
            string cep = "87023670";
            var endereco = new Endereco
            { 
                Cep = "87023670",
                Logradouro = "Rua Jacaranda",
                Bairro = "Parque Palmeiras",
                Cidade = "Maringa",
                Estado = "PR",
            };
            var enderecoDto = new EnderecoDto(endereco);

            _repository.GetByCep(cep).Returns(endereco);

            var result = _controller.GetByCep(cep);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<EnderecoDto>(okResult.Value);
            
            var enderecoResult = okResult.Value as EnderecoDto;
            Assert.Equal(enderecoDto.Cep, enderecoResult.Cep);
            Assert.Equal(enderecoDto.Logradouro, enderecoResult.Logradouro);
            Assert.Equal(enderecoDto.Bairro, enderecoResult.Bairro);
            Assert.Equal(enderecoDto.Cidade, enderecoResult.Cidade);
            Assert.Equal(enderecoDto.Estado, enderecoResult.Estado);
        }

        [Fact]
        public void Get_All()
        {
            var endereco1 = new Endereco
            { 
                Id = 1,
                Cep = "87023670",
                Logradouro = "Rua Jacaranda",
                Bairro = "Parque Palmeiras",
                Cidade = "Maringa",
                Estado = "PR",
            };
            var endereco2 = new Endereco
            { 
                Id = 2,
                Cep = "87023630",
                Logradouro = "Travessa Jurema",
                Bairro = "Jardim Vitória",
                Cidade = "Maringa",
                Estado = "PR",
            };
            var enderecos = new List<Endereco>
            {
                endereco1,
                endereco2
            };

            _repository.GetAll().Returns(enderecos);

            var result = _controller.GetAll();
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
