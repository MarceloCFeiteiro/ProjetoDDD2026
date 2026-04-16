#nullable enable
using Application;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain;
using Domain.Interfaces;
using Entities.Entidades;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace Application.Services.UnitTests
{
    public class EmpresaServiceTests
    {
        /// <summary>
        /// Verifies that when the repository returns a non-null Empresa the service maps all properties correctly to EmpresaDTO.
        /// Input conditions: Various Empresa instances (including boundary int Id values and strings with special/empty content).
        /// Expected result: Returned EmpresaDTO is not null and all properties are equal to the source Empresa properties.
        /// </summary>
        [Theory(DisplayName = "GetEmpresaByIdAsync - maps returned Empresa to EmpresaDTO")]
        [MemberData(nameof(MappingCases))]
        public async Task GetEmpresaByIdAsync_RepositoryReturnsEmpresa_ReturnsMappedEmpresaDTO(int id, Empresa empresa)
        {
            // Arrange
            var mockRepo = new Mock<IEmpresaRepository>(MockBehavior.Strict);
            mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(empresa);

            var service = new EmpresaService(mockRepo.Object);

            // Act
            var result = await service.GetEmpresaByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(empresa.Id);
            result.Ativo.Should().Be(empresa.Ativo);
            result.Documento.Should().Be(empresa.Documento);
            result.Nome.Should().Be(empresa.Nome);

            mockRepo.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        /// <summary>
        /// Verifies that when the repository returns null for a given id the service returns null.
        /// Input conditions: Various id values (including int.MinValue, -1, 0, int.MaxValue).
        /// Expected result: Returned value is null.
        /// </summary>
        [Theory(DisplayName = "GetEmpresaByIdAsync - returns null when repository returns null")]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public async Task GetEmpresaByIdAsync_RepositoryReturnsNull_ReturnsNull(int id)
        {
            // Arrange
            var mockRepo = new Mock<IEmpresaRepository>(MockBehavior.Strict);
            mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Empresa?)null);

            var service = new EmpresaService(mockRepo.Object);

            // Act
            var result = await service.GetEmpresaByIdAsync(id);

            // Assert
            result.Should().BeNull();
            mockRepo.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        /// <summary>
        /// Verifies that exceptions thrown by the repository are propagated by the service.
        /// Input conditions: repository throws InvalidOperationException for the requested id.
        /// Expected result: The same exception type and message is propagated to the caller.
        /// </summary>
        [Fact(DisplayName = "GetEmpresaByIdAsync - propagates exceptions from repository")]
        public async Task GetEmpresaByIdAsync_RepositoryThrows_ExceptionPropagated()
        {
            // Arrange
            const int id = 42;
            var expectedException = new InvalidOperationException("boom");
            var mockRepo = new Mock<IEmpresaRepository>(MockBehavior.Strict);
            mockRepo.Setup(r => r.GetByIdAsync(id)).ThrowsAsync(expectedException);

            var service = new EmpresaService(mockRepo.Object);

            // Act
            Func<Task> act = () => service.GetEmpresaByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("boom");
            mockRepo.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        // Test data providers

        public static IEnumerable<object[]> MappingCases()
        {
            // 1) Boundary Id: int.MinValue, special characters in strings
            yield return new object[]
            {
                int.MinValue,
                new Empresa
                {
                    Id = int.MinValue,
                    Ativo = true,
                    Documento = "⁂特殊字符\n\t",
                    Nome = "MinIdCompany"
                }
            };

            // 2) Zero Id, empty strings
            yield return new object[]
            {
                0,
                new Empresa
                {
                    Id = 0,
                    Ativo = false,
                    Documento = string.Empty,
                    Nome = " " // whitespace-only name
                }
            };

            // 3) Boundary Id: int.MaxValue, long strings
            yield return new object[]
            {
                int.MaxValue,
                new Empresa
                {
                    Id = int.MaxValue,
                    Ativo = false,
                    Documento = new string('D', 1024),
                    Nome = new string('N', 2048)
                }
            };
        }
    }
}