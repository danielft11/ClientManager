using ClientManagerBackend.Dominio.Entities;
using ClientManagerBackend.Infra.Data.Mapeamentos;
using Microsoft.EntityFrameworkCore;

namespace ClientManagerBackend.Infra.Data.Contexto
{
    public class ClientManagerContext : DbContext, IClientManagerContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        #region Construtores
        public ClientManagerContext(DbContextOptions<ClientManagerContext> options) : base(options)
        {}

        public ClientManagerContext()
        {}
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ClienteMapeamento());
        }

    }
}
