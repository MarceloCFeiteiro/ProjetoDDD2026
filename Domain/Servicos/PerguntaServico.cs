using Domain.Interfaces;
using Domain.InterfacesServicos;
using Entities.Entidades;
using Entities.EntidadesNoMap;

namespace Domain.Sevicos
{
    public class PerguntaServico : IPerguntaServico
    {
        private readonly IOpcaoRepository _iOpcaoRepository;

        private readonly IPerguntaRepository _iPerguntaRepository;

        public PerguntaServico(IOpcaoRepository iopcaoRepository, IPerguntaRepository iPerguntaRepository)
        {
            _iOpcaoRepository = iopcaoRepository;
            _iPerguntaRepository = iPerguntaRepository;
        }

        public async Task AdicionarPequisaOpcoes(PerguntaOpcoesDTO resposta)
        {
            var pergunta = new Pergunta
            {
                Ativo = resposta.Ativo,
                IdPesquisa = resposta.IdPesquisa,
                Nome = resposta.Nome
            };

            await _iPerguntaRepository.AddAsync(pergunta);

            foreach (var item in resposta.Opcoes)
            {
                var opcao = new Opcao
                {
                    Peso = item.Peso,
                    Ativo = item.Ativo,
                    IdPergunta = item.IdPergunta,
                    Nome = item.Nome
                };

                await _iOpcaoRepository.AddAsync(opcao);
            }
        }

        public async Task AtualizarPesquisaOpcoes(PerguntaOpcoesDTO resposta)
        {
            var pergunta = await _iPerguntaRepository.GetByIdAsync(resposta.Id);

            if (pergunta is null)
                throw new Exception();

            pergunta.Ativo = resposta.Ativo;

            await _iPerguntaRepository.UpdateAsync(pergunta);

            var opcoesAtuais = (await _iOpcaoRepository.GetAllAsync())
                .Where(o => o.IdPergunta == pergunta.Id)
                .ToList();

            foreach (var opcao in resposta.Opcoes)
            {
                if (opcao.Id > 0)
                {
                    var opcaoExistente = opcoesAtuais.FirstOrDefault(o => o.Id == opcao.Id);

                    if (opcaoExistente != null)
                    {
                        opcaoExistente.Peso = opcao.Peso;
                        opcaoExistente.Ativo = opcao.Ativo;
                        opcaoExistente.Nome = opcao.Nome;

                        await _iOpcaoRepository.UpdateAsync(opcaoExistente);
                        continue;
                    }
                }

                var opcaoNova = new Opcao
                {
                    Peso = opcao.Peso,
                    Ativo = opcao.Ativo,
                    IdPergunta = opcao.IdPergunta,
                    Nome = opcao.Nome
                };

                await _iOpcaoRepository.AddAsync(opcaoNova);
            }

            var idsRecebidos = resposta.Opcoes
                .Where(o => o.Id > 0)
                .Select(o => o.Id)
                .ToList();

            var opcoesRemovidas = opcoesAtuais
                .Where(o => !idsRecebidos.Contains(o.Id))
                .ToList();

            foreach (var opcao in opcoesRemovidas)
            {
                opcao.Ativo = false;

                await _iOpcaoRepository.UpdateAsync(opcao);
            }
        }

        public async Task<PerguntaOpcoesDTO> ObterPerguntaComOpcoes(uint idPergunta)
        {
            var pergunta = await _iPerguntaRepository.GetByIdAsync(idPergunta);

            if (pergunta == null)
                throw new Exception();

            var opcoes = (await _iOpcaoRepository.GetAllAsync())
                .Where(o => o.IdPergunta == pergunta.Id && o.Ativo == pergunta.Ativo)
                .ToList();

            var dto = new PerguntaOpcoesDTO
            {
                Id = pergunta.Id,
                Nome = pergunta.Nome,
                Ativo = pergunta.Ativo,
                IdPesquisa = pergunta.IdPesquisa,
                Opcoes = opcoes.Select(o => new OpcaoDTO
                {
                    Id = o.Id,
                    Nome = o.Nome,
                    Peso = o.Peso
                }).ToList()
            };

            return dto;
        }
    }
}
