using Domain.Interfaces;
using Entities.Entidades;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using WebApi.DTOs;
using WebApi.Models;
using WebApi.Requests.Empresa;
using WebApi.Swagger.Examples;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/empresas")]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaRepository _empresaRepository;

        //private readonly IRepository<Empresa> _repository; Maneira gebnérica de consumir

        public EmpresasController(IEmpresaRepository empresaRepository /*IRepository<Empresa> repository Maneira gebnérica de consumir*/)
        {
            _empresaRepository = empresaRepository;
            // _repository = repository;Maneira gebnérica de consumir
        }

        /// <summary>
        /// Busca uma empresa pelo ID
        /// </summary>
        /// <param name="id">ID da empresa (mínimo 1)</param>
        /// <response code="200">Empresa encontrada com sucesso</response>
        /// <response code="400">Erro de validação (ex: ID inválido)</response>
        /// <response code="404">Empresa não encontrada</response>
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmpresaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundEmpresaExample))]
        public async Task<ActionResult<EmpresaDTO>> GetEmpresasById(
            [FromRoute] GetEmpresaByIdRequest request)
        {

            var empresa = await _empresaRepository.GetByIdAsync(request.Id);


            if (empresa == null)
                return NotFound(new ErrorResponse
                {
                    Message = "Empresa não encontrada",
                    Errors = new[] { $"Nenhuma empresa com ID {request.Id} foi localizada." }
                });

            return Ok(new EmpresaDTO
            {
                Id = empresa.Id,
                Nome = empresa.Nome
            });
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
