using Swashbuckle.AspNetCore.Filters;
using WebApi.Models;

namespace WebApi.Swagger.Examples
{
    public class ValidationErrorExample : IExamplesProvider<ErrorResponse>
    {
        public ErrorResponse GetExamples()
        {
            return new ErrorResponse
            {
                Message = "Erro de validação",
                Errors = new[]
                {
                    "O ID deve ser maior que 0."
                }
            };
        }

    }
}