using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Entities.Entidades;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
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

        private readonly IEmpresaService _empresaService; // Injeção do serviço para lógica de negócios (opcional, dependendo da arquitetura)

        private readonly IValidator<GetEmpresaByIdRequest> _validator;

        public EmpresasController(IEmpresaRepository empresaRepository, IEmpresaService empresaService, IValidator<GetEmpresaByIdRequest> validator /*IRepository<Empresa> repository Maneira gebnérica de consumir*/)
        {
            _empresaRepository = empresaRepository;
            _empresaService = empresaService;
            // _repository = repository;Maneira gebnérica de consumir
            _validator = validator;
        }

        /// <summary>
        /// Retorna uma empresa a partir do seu identificador.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/empresas/1
        ///
        /// Regras:
        /// - O ID deve ser maior que 0
        /// - Retorna 404 caso a empresa não exista
        /// </remarks>
        /// <param name="id">Identificador único da empresa (ex: 1)</param>
        /// <response code="200">Empresa encontrada com sucesso</response>
        /// <response code="400">Erro de validação (ID inválido)</response>
        /// <response code="404">Empresa não encontrada</response>
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(EmpresaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationErrorExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundEmpresaExample))]
        public async Task<ActionResult<EmpresaDTO>> GetEmpresasById(int id)
        {

            var request = new GetEmpresaByIdRequest { Id = id };

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Erro de validação",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            var empresa = await _empresaService.GetEmpresaByIdAsync(request.Id);

            if (empresa == null)
            {
                return NotFound(new ErrorResponse
                {
                    Message = "Empresa não encontrada",
                    Errors = new[] { $"Nenhuma empresa com ID {request.Id} foi localizada." }
                });
            }

            return Ok(empresa);

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
