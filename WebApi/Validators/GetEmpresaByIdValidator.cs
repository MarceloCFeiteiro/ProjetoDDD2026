using FluentValidation;
using WebApi.Requests.Empresa;

namespace WebApi.Controllers.Validators
{
    public class GetEmpresaByIdValidator : AbstractValidator<GetEmpresaByIdRequest>
    {
        public GetEmpresaByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O ID deve ser maior que 0.");
        }
    }
}
