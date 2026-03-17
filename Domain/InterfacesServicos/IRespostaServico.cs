using Entities.EntidadesNoMap;

namespace Domain.InterfacesServicos
{
    public interface IRespostaServico
    {
        Task AdicionarRespostaOpcoes(RespostasEntrevista resposta);
    }
}
