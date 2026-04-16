using Application.Services;
using Domain.Interfaces;
using Entities.Entidades;
using FluentAssertions;
using Moq;

namespace Testes.ApplicationTests
{
    public class EmpresaServiceTests
    {
        private readonly Mock<IEmpresaRepository> _mockRepo;
        private readonly EmpresaService _service;

        public EmpresaServiceTests()
        {
            _mockRepo = new Mock<IEmpresaRepository>();
            _service = new EmpresaService(_mockRepo.Object);
        }

        [Fact]
        public async Task Deve_retornar_EmpresaDTO_quando_empresa_existir()
        {
            // Arrange
            var idEmpresa = 1;

            var empresa = new Empresa
            {
                Id = idEmpresa,
                Nome = "Empresa Teste",
                Documento = "12345678000100",
                Ativo = true
            };

            _mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(empresa);

            // Act
            var result = await _service.GetEmpresaByIdAsync(idEmpresa);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(idEmpresa);
            result.Nome.Should().Be("Empresa Teste");
            result.Documento.Should().Be("12345678000100");
            result.Ativo.Should().BeTrue();
        }

        [Fact]
        public async Task Deve_retornar_null_quando_empresa_nao_existir()
        {
            // Arrange
            var idEmpresa = 999;

            _mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Empresa?)null);

            // Act
            var result = await _service.GetEmpresaByIdAsync(idEmpresa);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Deve_chamar_repository_com_id_correto()
        {
            // Arrange
            var idEmpresa = 10;

            _mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Empresa());

            // Act
            await _service.GetEmpresaByIdAsync(idEmpresa);

            // Assert
            _mockRepo.Verify(x => x.GetByIdAsync(idEmpresa), Times.Once);
        }
    }
}