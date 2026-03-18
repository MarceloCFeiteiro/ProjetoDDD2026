using Domain.Interfaces;
using Entities.Entidades;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.DTOs;
using WebApi.Controllers.Requests.Empresa;

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

        /// <summary>
        /// Busca uma empresa pelo ID
        /// </summary>
        /// <param name="id">ID da empresa</param>
        [HttpGet("empresas/{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmpresaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Empresa>> GetEmpresasById([FromRoute] int id)
        {

            var request = new GetEmpresaByIdRequest { Id = id };

            var empresa = await _empresaRepository.GetByIdAsync(request.Id);


            if (empresa == null)
                return NotFound($"Empresa com ID {id} não encontrada.");

            var empresaDTO = empresa;

            return Ok(empresaDTO);
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
