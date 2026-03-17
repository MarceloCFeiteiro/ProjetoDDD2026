using Domain.InterfacesServicos;
using Entities.EntidadesNoMap;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespostaController : ControllerBase
    {
        private readonly IRespostaServico _respostaServico;

        public RespostaController(IRespostaServico respostaServico)
        {
            _respostaServico = respostaServico;
        }

        [HttpPost("/api/addRespostaPesquisa")]
        [Produces("application/json")]
        public async Task<object> addRespostaPesquisa(RespostasEntrevista respostasEntrevista)
        {
            await _respostaServico.AdicionarRespostaOpcoes(respostasEntrevista);

            return respostasEntrevista;
        }
    }
}
