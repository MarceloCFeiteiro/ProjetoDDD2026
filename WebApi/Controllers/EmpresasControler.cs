using Domain.Interfaces;
using Entities.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasControler : ControllerBase
    {
        private readonly IEmpresaRepository _empresaRepository;

        //private readonly IRepository<Empresa> _repository; Maneira gebnérica de consumir

        public EmpresasControler(IEmpresaRepository empresaRepository /*IRepository<Empresa> repository Maneira gebnérica de consumir*/)
        {
            _empresaRepository = empresaRepository;
            // _repository = repository;Maneira gebnérica de consumir
        }

        [HttpGet("/api/GetEmpresasById")]
        [Produces("application/json")]
        public async Task<object> GetEmpresasById(uint id)
        {
            var empresas = await _empresaRepository.GetByIdAsync(id);

            return Ok(empresas);
        }

        [HttpGet("/api/GetAllEmpresas")]
        [Produces("application/json")]
        public async Task<object> GetAllEmpresas()
        {
            var empresas = await _empresaRepository.GetAllAsync();

            return Ok(empresas);
        }

        [HttpPost("/api/CreateEmpresa")]
        [Produces("application/json")]
        public async Task<object> CreateEmpresa([FromBody] Empresa empresa)
        {
            await _empresaRepository.AddAsync(empresa);

            return Ok(empresa);
        }

        [HttpPost("/api/UpdateEmpresa")]
        [Produces("application/json")]
        public async Task<object> UpdateEmpresa([FromBody] Empresa empresa)
        {
            await _empresaRepository.UpdateAsync(empresa);

            return Ok(empresa);
        }
    }
}
