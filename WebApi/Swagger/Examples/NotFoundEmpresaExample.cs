using Swashbuckle.AspNetCore.Filters;
using WebApi.Models;

namespace WebApi.Swagger.Examples
{
    public class NotFoundEmpresaExample : IExamplesProvider<ErrorResponse>
    {
        public ErrorResponse GetExamples()
        {
            return new ErrorResponse
            {
                Message = "Empresa não encontrada",
                Errors = new[] { "Nenhuma empresa com ID {id} foi localizada" }
            };
        }
    }
}
