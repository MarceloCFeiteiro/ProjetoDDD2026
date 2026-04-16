using Application.DTOs;

namespace Application.Interfaces
{
    public interface IEmpresaService
    {
        Task<EmpresaDTO?> GetEmpresaByIdAsync(int id);
    }
}
