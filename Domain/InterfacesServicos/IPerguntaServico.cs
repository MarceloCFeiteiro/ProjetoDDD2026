using Entities.EntidadesNoMap;

namespace Domain.InterfacesServicos
{
    public interface IPerguntaServico
    {
        Task AdicionarPequisaOpcoes(PerguntaOpcoesDTO resposta);

        Task AtualizarPesquisaOpcoes(PerguntaOpcoesDTO resposta);

        Task<PerguntaOpcoesDTO> ObterPerguntaComOpcoes(int idPergunta);
    }
}