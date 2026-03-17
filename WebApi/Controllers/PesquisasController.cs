using Domain.Interfaces;
using Entities.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PesquisasController : ControllerBase
    {
        private readonly IPesquisaRepository _pesquisaRepository;

        public PesquisasController(IPesquisaRepository pesquisaRepository)
        {
            _pesquisaRepository = pesquisaRepository;
        }

        [HttpGet("api/GetAllPesquisa")]
        [Produces("application/json")]
        public async Task<object> GetAllPesquisa()
        {
            return await _pesquisaRepository.GetAllAsync();
        }

        [HttpPost("api/AddPesquisa")]
        [Produces("application/json")]
        public async Task<object> AddPesquisa(Pesquisa pesquisa)
        {
            await _pesquisaRepository.AddAsync(pesquisa);

            return pesquisa;
        }

        [HttpPost("api/UpdatePesquisa")]
        [Produces("application/json")]
        public async Task<object> UpdatePesquisa(Pesquisa pesquisa)
        {
            await _pesquisaRepository.UpdateAsync(pesquisa);

            return pesquisa;
        }

        [HttpGet("api/GetPesquisaById")]
        [Produces("application/json")]
        public async Task<object> GetPesquisaById(uint id)
        {
            return await _pesquisaRepository.GetByIdAsync(id);
        }

        [HttpDelete("/api/DeletePesquisa")]
        [Produces("application/json")]
        public async Task<object> DeletePergunta(uint id)
        {
            try
            {
                var categoria = await _pesquisaRepository.GetByIdAsync(id);

                await _pesquisaRepository.DeleteAsync(categoria);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
