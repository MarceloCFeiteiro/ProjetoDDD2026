using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Entities.Entidades;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Controllers.Validators;
using WebApi.Models;

namespace Testes.ControllerTests
{
    public class EmpresasControllerTests
    {
        private readonly Mock<IEmpresaRepository> _mockRepo;

        private readonly Mock<IEmpresaService> _mockService;
        private readonly GetEmpresaByIdValidator _validator;
        private readonly EmpresasController _controller;

        public EmpresasControllerTests()
        {
            _mockRepo = new Mock<IEmpresaRepository>();
            _mockService = new Mock<IEmpresaService>();
            _validator = new GetEmpresaByIdValidator();
            _controller = new EmpresasController(_mockRepo.Object, _mockService.Object, _validator);

        }
        #region GetEmpresasById
        [Fact]
        public async Task Deve_retornar_200_quando_empresa_existir()
        {
            // Arrange
            var idEmpresa = 1;
            var nomeEmpresa = "Nome Teste";

            _mockService.Setup(x => x.GetEmpresaByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new EmpresaDTO { Id = idEmpresa, Nome = nomeEmpresa });

            // Act
            var result = await _controller.GetEmpresasById(idEmpresa);

            // Assert
            var okResult = result.Result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var empresaDTO = okResult.Value as EmpresaDTO;
            empresaDTO.Should().NotBeNull();
            empresaDTO.Id.Should().Be(idEmpresa);
            empresaDTO.Nome.Should().Be(nomeEmpresa);
        }

        [Fact]
        public async Task Deve_retornar_400_quando_id_for_invalido()
        {
            // Arrange
            // Act
            var result = await _controller.GetEmpresasById(0);

            // Assert
            var badRequest = result.Result as BadRequestObjectResult;
            badRequest.Should().NotBeNull();
            badRequest.StatusCode.Should().Be(400);

            var response = badRequest.Value as ErrorResponse;
            response.Should().NotBeNull();
            response.Message.Should().Be("Erro de validação");
            response.Errors.Should().Contain("O ID deve ser maior que 0.");
        }

        [Fact]
        public async Task Deve_retornar_404_quando_empresa_nao_existir()
        {
            // Arrange
            var idEmpresa = 99999;

            _mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Empresa)null);

            // Act
            var result = await _controller.GetEmpresasById(idEmpresa);

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);

            var response = notFoundResult.Value as ErrorResponse;
            response.Should().NotBeNull();
            response.Message.Should().Be("Empresa não encontrada");
            response.Errors.Should().Contain($"Nenhuma empresa com ID {idEmpresa} foi localizada.");
        }
        #endregion

        public async Task teste()
        {
            "1".Should().Be("2");
        }
    }
}

