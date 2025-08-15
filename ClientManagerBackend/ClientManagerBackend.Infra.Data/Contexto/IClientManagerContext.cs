using ClientManagerBackend.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace ClientManagerBackend.Infra.Data.Contexto
{
    public interface IClientManagerContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
