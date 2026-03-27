using FluentAssertions;
using WebApi.Controllers.Validators;
using WebApi.Requests.Empresa;

namespace Testes.ValidatorTests
{
    public class GetEmpresaByIdValidatorTest
    {
        private readonly GetEmpresaByIdValidator _validator;

        public GetEmpresaByIdValidatorTest()
        {
            _validator = new GetEmpresaByIdValidator();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Deve_retornar_erro_quando_id_for_menor_igual_a_zero(int id)
        {

            // Arrange
            var request = new GetEmpresaByIdRequest { Id = id };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Select(e => e.ErrorMessage)
             .Should().Contain("O ID deve ser maior que 0.");
        }

        [Fact]
        public void Deve_ser_valido_quando_id_for_maior_que_0()
        {
            // Arrange
            var request = new GetEmpresaByIdRequest { Id = 1 };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void teste()
        {
            var teste = 1 ;

            teste.Should().Be(1);
        }
    }
}
