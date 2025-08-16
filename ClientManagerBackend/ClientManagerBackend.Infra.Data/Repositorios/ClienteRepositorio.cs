using ClientManagerBackend.Dominio.Entities;
using ClientManagerBackend.Dominio.Interfaces;
using ClientManagerBackend.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBackend.Infra.Data.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IClientManagerContext _contexto;

        public ClienteRepositorio(IClientManagerContext contexto) 
        {
            _contexto = contexto;
        }

        public async Task<IList<Cliente>> GetCustomersAsync()
        {
            return await _contexto.Clientes.ToListAsync();
        }
        
        public async Task<Cliente> GetCustomerByCpfAsync(string cpf)
        {
            return await _contexto.Clientes.FirstOrDefaultAsync(c => c.Cpf.Equals(cpf));
        }

        public async Task<Cliente> AddCustomerAsync(Cliente cliente)
        {
            _contexto.Clientes.Add(cliente);
            await _contexto.SaveChangesAsync();

            return cliente;
        }

        public async Task<Cliente> UpdateCustomerAsync(Cliente cliente)
        {
            _contexto.Clientes.Update(cliente);
            await _contexto.SaveChangesAsync();

            return cliente;
        }
        
        public async Task DeleteCustomerAsync(Cliente cliente)
        {
            _contexto.Clientes.Remove(cliente);
            await _contexto.SaveChangesAsync();
        }
    
    }

}
