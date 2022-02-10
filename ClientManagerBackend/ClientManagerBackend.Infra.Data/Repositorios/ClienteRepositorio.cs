using ClientManagerBackend.Dominio.Entidades;
using ClientManagerBackend.Dominio.Interfaces;
using ClientManagerBackend.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBackend.Infra.Data.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ClientManagerContext _contexto;

        public ClienteRepositorio(ClientManagerContext contexto) 
        {
            _contexto = contexto;
        }

        public async Task<IList<Cliente>> ObterClientesAsync()
        {
            return await _contexto.Clientes.ToListAsync();
        }
        
        public async Task<Cliente> ObterClientePeloCPF(string cpf)
        {
            return await _contexto.Clientes.FirstOrDefaultAsync(c => c.Cpf.Equals(cpf));
        }

        public async Task<Cliente> CadastrarClienteAsync(Cliente cliente)
        {
            _contexto.Clientes.Add(cliente);
            await _contexto.SaveChangesAsync();

            return cliente;
        }

        public async Task<Cliente> AtualizarClienteAsync(Cliente cliente)
        {
            _contexto.Clientes.Update(cliente);
            await _contexto.SaveChangesAsync();

            return cliente;
        }
        
        public async Task<Cliente> DeletarClienteAsync(Cliente cliente)
        {
            _contexto.Clientes.Remove(cliente);
            await _contexto.SaveChangesAsync();

            return cliente;
        }
    
    }

}
