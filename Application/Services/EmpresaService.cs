using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<EmpresaDTO?> GetEmpresaByIdAsync(int id)
        {
            var empresa = await _empresaRepository.GetByIdAsync(id);

            if (empresa == null)
                return null;

            return new EmpresaDTO
            {
                Id = empresa.Id,
                Ativo = empresa.Ativo,
                Documento = empresa.Documento,
                Nome = empresa.Nome
            };
        }
    }
}
