using Domain.Interfaces;
using Domain.InterfacesServicos;
using Domain.Sevicos;
using Infra.Repositorios.Genericos;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Repositorios
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddEfRepositories(this IServiceCollection services)
        {
            // Repositórios específicos
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IOpcaoRepository, OpcaoRepository>();
            services.AddScoped<IOpcaoRespostaRepository, OpcaoRespostaRepository>();
            services.AddScoped<IPerguntaRepository, PerguntaRepository>();
            services.AddScoped<IPesquisaRepository, PesquisaRepository>();
            services.AddScoped<IRespostaRepository, RespostaRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Services 
            services.AddScoped<IPerguntaServico, PerguntaServico>();
            services.AddScoped<IRespostaServico, RespostaServico>();

            return services;

        }
    }
}
