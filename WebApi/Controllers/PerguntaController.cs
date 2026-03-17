using Domain.Interfaces;
using Domain.InterfacesServicos;
using Entities.Entidades;
using Entities.EntidadesNoMap;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerguntaController : ControllerBase
    {
        private readonly IPerguntaRepository _perguntaRepository;

        private readonly IPerguntaServico _perguntaServico;

        public PerguntaController(IPerguntaRepository perguntaRepository, IPerguntaServico perguntaServico)
        {
            _perguntaRepository = perguntaRepository;
            _perguntaServico = perguntaServico;
        }

        [HttpGet("api/GetPerguntaById")]
        [Produces("application/json")]
        public async Task<object> GetPerguntaById(uint id)
        {
            var pergunta = await _perguntaRepository.GetByIdAsync(id);

            return Ok(pergunta);
        }

        [HttpGet("api/GetAllPerguntas")]
        [Produces("application/json")]
        public async Task<object> GetAllPerguntas()
        {
            var perguntas = await _perguntaRepository.GetAllAsync();

            return Ok(perguntas);
        }

        [HttpGet("api/ObterPerguntaComOpcoes/{idPergunta}")]
        [Produces("application/json")]
        public async Task<object> ObterPerguntaComOpcoes(uint idPergunta)
        {
            var resultado = await _perguntaServico.ObterPerguntaComOpcoes(idPergunta);

            return Ok(resultado);
        }

        [HttpPost("api/AdicionarPesquisaOpcoes")]
        [Produces("application/json")]
        public async Task<object> AdicionarPesquisaOpcoes(PerguntaOpcoesDTO perguntaOpcoesDTO)
        {
            await _perguntaServico.AdicionarPequisaOpcoes(perguntaOpcoesDTO);

            return Ok(perguntaOpcoesDTO);
        }

        [HttpPost("api/AtualizarPesquisaOpcoes")]
        [Produces("application/json")]
        public async Task<object> AtualizarPesquisaOpcoes(PerguntaOpcoesDTO perguntaOpcoesDTO)
        {
            await _perguntaServico.AtualizarPesquisaOpcoes(perguntaOpcoesDTO);

            return Ok(perguntaOpcoesDTO);
        }


        [HttpPost("api/UpdatePergunta")]
        [Produces("application/json")]
        public async Task<object> UpdatePergunta(Pergunta pergunta)
        {
            await _perguntaRepository.UpdateAsync(pergunta);

            return pergunta;
        }

        [HttpDelete("/api/DeletePergunta")]
        [Produces("application/json")]
        public async Task<object> DeletePergunta(uint id)
        {
            try
            {
                var categoria = await _perguntaRepository.GetByIdAsync(id);
                
                await _perguntaRepository.DeleteAsync(categoria);
            }
            catch (Exception) 
            {
                return false;
            }

            return true;
        }
    }
}
