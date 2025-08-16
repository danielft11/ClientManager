using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using ClientManagerBackend.Dominio.Entities;

namespace ClientManagerBackend.Infra.Data.Contexto
{
    public interface IClientManagerContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
