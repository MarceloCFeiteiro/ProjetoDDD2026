using Domain.Interfaces;
using Entities.Entidades;
using Infra.Config;
using Infra.Repositorios.Genericos;

namespace Infra.Repositorios
{
    public class OpcaoRepository : Repository<Opcao>, IOpcaoRepository
    {
        public OpcaoRepository(PesquisaContext context) : base(context)
        {
        }
    }
}
