using ClientManagerBackend.Aplicacao.Automapper;
using ClientManagerBackend.Aplicacao.Interfaces;
using ClientManagerBackend.Aplicacao.Servicos;
using ClientManagerBackend.Dominio.Interfaces;
using ClientManagerBackend.Infra.Data.Contexto;
using ClientManagerBackend.Infra.Data.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClientManagerBackend.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void AdicionarContextoDB(this IServiceCollection services)
        {
            services.AddDbContext<ClientManagerContext>(options => {
                options.UseInMemoryDatabase("CliManagerDbContextInMemory");
                options.EnableSensitiveDataLogging();
            });
        }

        public static void AdicionarServicos(this IServiceCollection services) 
        {
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IClienteServico, ClienteServico>();
        }

        public static void AdicionarAutoMapper(this IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(DominioParaDTOMapping));
        }

    }

}
