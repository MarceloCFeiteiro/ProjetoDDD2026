using Domain.Interfaces;
using Domain.InterfacesServicos;
using Entities.Entidades;
using Entities.EntidadesNoMap;

namespace Domain.Sevicos
{
    public class RespostaServico : IRespostaServico
    {
        private readonly IOpcaoRespostaRepository _iOpcaoRespostaRepository;

        private readonly IRespostaRepository _iRespostaRepository;

        public RespostaServico(IOpcaoRespostaRepository iOpcaoRespostaRepository, IRespostaRepository iRespostaRepository)
        {
            _iOpcaoRespostaRepository = iOpcaoRespostaRepository;
            _iRespostaRepository = iRespostaRepository;
        }

        public async Task AdicionarRespostaOpcoes(RespostasEntrevista resposta)
        {
            foreach (var item in resposta.ListaRespostaPergunta)
            {
                var respostaNova = new Resposta
                {
                    CpfEntrevistado = item.CpfEntrevistado,
                    NomeEntrevistado = item.NomeEntrevistado,
                    IdEmpresa = item.IdEmpresa
                };

                await _iRespostaRepository.AddAsync(respostaNova);

                var opcaoResposta = new OpcaoResposta
                {
                    IdOpcao = item.opcaoResposta.Id,
                    IdResposta = respostaNova.Id
                };

                await _iOpcaoRespostaRepository.AddAsync(opcaoResposta);
            }
        }
    }
}
